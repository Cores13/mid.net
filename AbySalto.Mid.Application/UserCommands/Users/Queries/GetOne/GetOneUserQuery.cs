using AbySalto.Mid.Application.Abstractions.Messaging;
using AbySalto.Mid.Domain.DTOs.Responses;

namespace AbySalto.Mid.Application.UserCommands.Users.Queries.GetOne
{
    public record GetOneUserQuery(int Id) : IQuery<UserResponseDto>;
}
