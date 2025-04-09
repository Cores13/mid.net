using AbySalto.Mid.Domain.DTOs.Requests;
using AbySalto.Mid.Domain.DTOs.Responses;
using AbySalto.Mid.Domain.Entities;

namespace AbySalto.Mid.Application.Mappers
{
    public static class CartMapper
    {
        public static Cart ToModel(this CartRequestDto cart)
        {
            return new Cart
            {
                Id = cart.Id,
                Total = cart.Total,
                DiscountedTotal = cart.DiscountedTotal,
                UserId = cart.UserId,
                TotalProducts = cart.TotalProducts,
                TotalQuantity = cart.TotalQuantity,
            };
        }

        public static CartResponseDto ToDto(this Cart cart)
        {
            return new CartResponseDto
            {
                Id = cart.Id,
                Products = cart.Products?.ToCartProductDto(),
                //Products = (List<CartProductResponseDto>)cart.Products?.ToCartProductDto(),
                Total = cart.Total,
                DiscountedTotal = cart.DiscountedTotal,
                UserId = cart.UserId,
                TotalProducts = cart.TotalProducts,
                TotalQuantity = cart.TotalQuantity,
            };
        }

        public static ICollection<Cart> ToModel(this IEnumerable<CartRequestDto> carts) => carts.Select(x => x.ToModel()).ToList();

        public static ICollection<CartResponseDto> ToDto(this IEnumerable<Cart> carts) => carts.Select(x => x.ToDto()).ToList();
    }

}
