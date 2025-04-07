using AbySalto.Mid.Application.Abstractions.Messaging;
using AbySalto.Mid.Domain.DTOs.Responses;

namespace AbySalto.Mid.Application.UserCommands.Products.Queries.GetAll
{
    public record GetAllProductsQuery() : IQuery<ICollection<ProductResponseDto?>>;
}
