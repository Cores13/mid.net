using AbySalto.Mid.Application.Abstractions.Messaging;
using AbySalto.Mid.Domain.DTOs.Responses;

namespace AbySalto.Mid.Application.UserCommands.Products.Queries.GetOne
{
    public record GetOneProductQuery(int Id) : IQuery<ProductResponseDto?>;
}
