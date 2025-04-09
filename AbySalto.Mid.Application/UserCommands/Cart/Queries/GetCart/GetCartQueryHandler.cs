using AbySalto.Mid.Application.Abstractions.Messaging;
using AbySalto.Mid.Application.Mappers;
using AbySalto.Mid.Application.UserCommands.Products.Queries.GetOne;
using AbySalto.Mid.Domain.Core.Errors;
using AbySalto.Mid.Domain.Core.Primitives;
using AbySalto.Mid.Domain.DTOs.Responses;
using AbySalto.Mid.Domain.Interfaces.Repository;
using AbySalto.Mid.Domain.Interfaces.Services;

namespace AbySalto.Mid.Application.UserCommands.Cart.Queries.GetCart
{
    internal sealed class GetCartQueryHandler : IQueryHandler<GetCartQuery, CartResponseDto?>
    {
        private readonly ICartRepository _cartRepository;

        public GetCartQueryHandler(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<Result<CartResponseDto?>> Handle(GetCartQuery request, CancellationToken cancellationToken = default)
        {
            var cart = await _cartRepository.GetByUserIdAsync(request.Id, cancellationToken);

            if (cart is null)
            {
                cart = new Domain.Entities.Cart
                {
                    Total = 0,
                    DiscountedTotal = 0,
                    UserId = request.Id,
                    TotalQuantity = 0,
                    TotalProducts = 0,
                };

                _cartRepository.Add(cart);
                _cartRepository.GetAddedEntity(cart);
            }

            return Result.Success(cart.ToDto());
        }
    }
}
