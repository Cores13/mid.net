using AbySalto.Mid.Domain.DTOs.Requests;
using AbySalto.Mid.Domain.DTOs.Responses;
using AbySalto.Mid.Domain.Entities;

namespace AbySalto.Mid.Application.Mappers
{
    public static class DimensionsMapper
    {
        public static Dimensions ToModel(this DimensionsRequestDto dimensions)
        {
            return new Dimensions
            {
                Width = dimensions.Width,
                Height = dimensions.Height,
                Depth = dimensions.Depth,
                ProductId = dimensions.ProductId,
            };
        }

        public static DimensionsResponseDto ToDto(this Dimensions dimensions)
        {
            return new DimensionsResponseDto
            {
                Id = dimensions.Id,
                Width = dimensions.Width,
                Height = dimensions.Height,
                Depth = dimensions.Depth,
                ProductId = dimensions.ProductId,
            };
        }

        public static ICollection<Dimensions> ToModel(this IEnumerable<DimensionsRequestDto> dimensions) => dimensions.Select(x => x.ToModel()).ToList();

        public static ICollection<DimensionsResponseDto> ToDto(this IEnumerable<Dimensions> dimensions) => dimensions.Select(x => x.ToDto()).ToList();
    }

}
