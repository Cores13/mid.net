using AbySalto.Mid.Application.Abstractions.Messaging;
using AbySalto.Mid.Domain.Core.Errors;
using AbySalto.Mid.Domain.Core.Primitives;
using AbySalto.Mid.Domain.DTOs.Responses;
using AbySalto.Mid.Domain.Interfaces.Repository;
using AbySalto.Mid.Domain.Interfaces.Services;

namespace AbySalto.Mid.Application.UserCommands.Cart.Commands.RemoveFromCart
{
    internal sealed class RemoveFromCartCommandHandler : ICommandHandler<RemoveFromCartCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICartRepository _cartRepository;

        public RemoveFromCartCommandHandler(IProductRepository productRepository, ICartRepository cartRepository)
        {
            _productRepository = productRepository;
            _cartRepository = cartRepository;
        }

        public async Task<Result> Handle(RemoveFromCartCommand request, CancellationToken cancellationToken = default)
        {
            var cart = await _cartRepository.GetByUserIdAsync(request.UserId, cancellationToken);
            
            if(cart is null)
            {
                return Result.Failure<ProductResponseDto?>(
                    DomainErrors.Cart.DoesNotExist);
            }

            var cartItem = await _cartRepository.GetProductFromCart(request.ProductId, (int)cart.Id, cancellationToken);

            if (cartItem is null)
            {
                return Result.Failure<ProductResponseDto?>(
                    DomainErrors.CartItem.DoesNotExist);
            }

            if(request.Quantity >= cartItem.Quantity)
            {
                if (cart.TotalProducts == 1)
                {
                    cart.Total = 0;
                    cart.TotalProducts = 0;
                    cart.TotalQuantity = 0;
                    cart.DiscountedTotal = 0;
                }
                else
                {
                    cart.Total -= cartItem.Total;
                    cart.TotalProducts -= 1;
                    cart.TotalQuantity -= cartItem.Quantity;
                    cart.DiscountedTotal -= Math.Round(cartItem.Total - cartItem.DiscountTotal, 2);
                }
                _cartRepository.RemoveFromCart(cartItem);
            }else
            {
                var product = await _productRepository.GetByApiIdAsync(request.ProductId, cancellationToken);

                cart.Total -= product.Price * request.Quantity;
                cart.TotalQuantity -= request.Quantity;
                cart.DiscountedTotal -= Math.Round(request.Quantity * product.Price * (1 - product.DiscountPercentage / 100), 2);

                cartItem.Quantity -= request.Quantity;
                cartItem.Total -= product.Price * request.Quantity;
                cartItem.DiscountTotal -= Math.Round(product.Price * request.Quantity * (product.DiscountPercentage / 100), 2);
            }


            return Result.Success();
        }
    }
}
