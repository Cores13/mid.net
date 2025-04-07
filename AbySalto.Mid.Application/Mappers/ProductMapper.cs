using AbySalto.Mid.Domain.DTOs.Requests;
using AbySalto.Mid.Domain.DTOs.Responses;
using AbySalto.Mid.Domain.Enums;
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
                Dimensions = product.Dimensions,
                WarrantyInformation = product.WarrantyInformation,
                ShippingInformation = product.ShippingInformation,
                AvailabilityStatus = product.AvailabilityStatus,
                Reviews = product.Reviews,
                ReturnPolicy = product.ReturnPolicy,
                MinimumOrderQuantity = product.MinimumOrderQuantity,
                Meta = product.Meta,
                Thumbnail = product.Thumbnail,
                Images = product.Images,

            };
        }

        public static ProductResponseDto ToDto(this Product product)
        {
            return new ProductResponseDto
            {
                Id = product.Id,
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
                Dimensions = product.Dimensions,
                WarrantyInformation = product.WarrantyInformation,
                ShippingInformation = product.ShippingInformation,
                AvailabilityStatus = product.AvailabilityStatus,
                Reviews = product.Reviews,
                ReturnPolicy = product.ReturnPolicy,
                MinimumOrderQuantity = product.MinimumOrderQuantity,
                Meta = product.Meta,
                Thumbnail = product.Thumbnail,
                Images = product.Images,
            };
        }

        public static ICollection<Product> ToModel(this IEnumerable<ProductRequestDto> products) => products.Select(x => x.ToModel()).ToList();

        public static ICollection<ProductResponseDto> ToDto(this IEnumerable<Product> products) => products.Select(x => x.ToDto()).ToList();
    }

}
