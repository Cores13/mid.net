using AbySalto.Mid.Domain.DTOs.Requests;
using AbySalto.Mid.Domain.DTOs.Responses;
using AbySalto.Mid.Domain.Entities;

namespace AbySalto.Mid.Application.Mappers
{
    public static class ProductMapper
    {
        public static Product ToModel(this ProductRequestDto product)
        {
            return new Product
            {
                Id = product.Id,
                ApiId = product.ApiId,
                Title = product.Title,
                Description = product.Description,
                Category = product.Category,
                Price = product.Price,
                DiscountPercentage = product.Price,
                Rating = product.Rating,
                Stock = product.Stock,
                Tags = product.Tags,
                Brand = product.Brand,
                Sku = product.Sku,
                Weight = product.Weight,
                Dimensions = product.Dimensions.ToModel(),
                WarrantyInformation = product.WarrantyInformation,
                ShippingInformation = product.ShippingInformation,
                AvailabilityStatus = product.AvailabilityStatus,
                Reviews = product.Reviews.ToModel(),
                ReturnPolicy = product.ReturnPolicy,
                MinimumOrderQuantity = product.MinimumOrderQuantity,
                Meta = product.Meta.ToModel(),
                Thumbnail = product.Thumbnail,
                Images = product.Images,
            };
        }

        public static ProductResponseDto ToDto(this Product product)
        {
            return new ProductResponseDto
            {
                Id = product.Id,
                ApiId = product.ApiId,
                Title = product.Title,
                Description = product.Description,
                Category = product.Category,
                Price = product.Price,
                DiscountPercentage = product.DiscountPercentage,
                Rating = product.Rating,
                Stock = product.Stock,
                Tags = product.Tags,
                Brand = product.Brand,
                Sku = product.Sku,
                Weight = product.Weight,
                Dimensions = product.Dimensions.ToDto(),
                WarrantyInformation = product.WarrantyInformation,
                ShippingInformation = product.ShippingInformation,
                AvailabilityStatus = product.AvailabilityStatus,
                Reviews = product.Reviews.ToDto(),
                ReturnPolicy = product.ReturnPolicy,
                MinimumOrderQuantity = product.MinimumOrderQuantity,
                Meta = product.Meta.ToDto(),
                Thumbnail = product.Thumbnail,
                Images = product.Images,
            };
        }

        public static CartProductResponseDto ToCartProductDto(this CartProduct product)
        {
            return new CartProductResponseDto
            {
                Id = product.Id,
                ProductId = (int)product.ProductId,
                CartId = (int)product.CartId,
                Title = product.Product.Title,
                Price = product.Product.Price,
                Quantity = product.Quantity,
                Total = product.Total,
                DiscountPercentage = product.Product.DiscountPercentage,
                DiscountTotal = product.DiscountTotal,
                Thumbnail = product.Product.Thumbnail,
            };
        }

        public static ICollection<Product> ToModel(this IEnumerable<ProductRequestDto> products) => products.Select(x => x.ToModel()).ToList();

        public static ICollection<ProductResponseDto> ToDto(this IEnumerable<Product> products) => products.Select(x => x.ToDto()).ToList();
        
        public static ICollection<CartProductResponseDto> ToCartProductDto(this IEnumerable<CartProduct> products) => products.Select(x => x.ToCartProductDto()).ToList();
    }

}
