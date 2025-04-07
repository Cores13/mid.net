using AbySalto.Mid.Application.Abstractions.Messaging;
using AbySalto.Mid.Domain.DTOs.Paging;
using AbySalto.Mid.Domain.DTOs.Responses;

namespace AbySalto.Mid.Application.UserCommands.Users.Queries.GetAllPaginated
{
    public record GetAllUsersPaginatedQuery(PagedRequest<string> PagedQuery) : IQuery<PagedResponse<UserResponseDto?>>;
}
