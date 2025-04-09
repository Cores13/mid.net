using AbySalto.Mid.Application.Abstractions.Messaging;
using AbySalto.Mid.Domain.Core.Errors;
using AbySalto.Mid.Domain.Core.Primitives;
using AbySalto.Mid.Domain.DTOs.Responses;
using AbySalto.Mid.Domain.Entities;
using AbySalto.Mid.Domain.Interfaces.Repository;
using AbySalto.Mid.Domain.Interfaces.Services;
using static AbySalto.Mid.Domain.Core.Errors.DomainErrors;

namespace AbySalto.Mid.Application.UserCommands.Cart.Commands.AddToCart
{
    internal sealed class AddToCartCommandHandler : ICommandHandler<AddToCartCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IExternalApiService _externalApiService;

        public AddToCartCommandHandler(IProductRepository productRepository, IExternalApiService externalApiService, ICartRepository cartRepository)
        {
            _productRepository = productRepository;
            _externalApiService = externalApiService;
            _cartRepository = cartRepository;
        }

        public async Task<Result> Handle(AddToCartCommand request, CancellationToken cancellationToken = default)
        {
            var product = await _productRepository.GetByApiIdAsync(request.ProductId, cancellationToken);

            if (product is null)
            {
                // Get from external source
                var externalProduct = await _externalApiService.GetProductById(request.ProductId);
                if (externalProduct is null)
                {
                    return Result.Failure<ProductResponseDto?>(DomainErrors.Product.DoesNotExist);
                }

                externalProduct.ApiId = externalProduct.Id;
                externalProduct.Id = null;

                _productRepository.Add(externalProduct);
                _productRepository.GetAddedEntity(externalProduct);

                product = externalProduct;
            }

            if (product is null)
            {
                return Result.Failure<ProductResponseDto?>(
                    DomainErrors.Product.DoesNotExist);
            }

            var cart = await _cartRepository.GetByUserIdAsync(request.UserId, cancellationToken);
            if(cart is null)
            {
                cart = new Domain.Entities.Cart
                {
                    Total = 0,
                    DiscountedTotal = 0,
                    UserId = request.UserId,
                    TotalQuantity = 0,
                    TotalProducts = 0,
                };
                
                _cartRepository.Add(cart);
                _cartRepository.GetAddedEntity(cart);
            }

            cart.Total += (request.Quantity * product.Price);
            cart.DiscountedTotal += Math.Round(request.Quantity * product.Price * (1 - product.DiscountPercentage / 100), 2);
            cart.UserId = request.UserId;
            cart.TotalQuantity += request.Quantity;

            var cartItem = await _cartRepository.GetProductFromCart(request.ProductId, (int)cart.Id, cancellationToken);

            if (cartItem is null)
            {
                cartItem = new CartProduct
                {
                    CartId = cart.Id,
                    ProductId = request.ProductId,
                    Quantity = request.Quantity,
                    Total = product.Price * request.Quantity,
                    DiscountTotal = Math.Round(product.Price * request.Quantity * (product.DiscountPercentage / 100), 2),
                    
                };
                _cartRepository.AddToCart(cartItem);
                cart.TotalProducts += 1;
            }
            else
            {
                cartItem.Quantity += request.Quantity;
                cartItem.Total += product.Price * request.Quantity;
                cartItem.DiscountTotal += Math.Round(product.Price * request.Quantity * (product.DiscountPercentage / 100), 2);
            }


            return Result.Success();
        }
    }
}
