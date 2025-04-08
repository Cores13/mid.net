using AbySalto.Mid.Domain.DTOs.Requests;
using AbySalto.Mid.Domain.DTOs.Responses;
using AbySalto.Mid.Domain.Entities;

namespace AbySalto.Mid.Application.Mappers
{
    public static class MetaMapper
    {
        public static Meta ToModel(this MetaRequestDto meta)
        {
            return new Meta
            {
                Barcode = meta.Barcode,
                QrCode = meta.QrCode,
                ProductId = meta.ProductId,
            };
        }

        public static MetaResponseDto ToDto(this Meta meta)
        {
            return new MetaResponseDto
            {
                Id = meta.Id,
                Barcode = meta.Barcode,
                QrCode = meta.QrCode,
                ProductId = meta.ProductId,
            };
        }

        public static ICollection<Meta> ToModel(this IEnumerable<MetaRequestDto> metas) => metas.Select(x => x.ToModel()).ToList();

        public static ICollection<MetaResponseDto> ToDto(this IEnumerable<Meta> metas) => metas.Select(x => x.ToDto()).ToList();
    }

}
