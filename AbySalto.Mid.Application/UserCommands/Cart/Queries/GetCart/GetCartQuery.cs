using AbySalto.Mid.Application.Abstractions.Messaging;
using AbySalto.Mid.Domain.DTOs.Responses;

namespace AbySalto.Mid.Application.UserCommands.Cart.Queries.GetCart
{
    public record GetCartQuery(int Id) : IQuery<CartResponseDto?>;
}
