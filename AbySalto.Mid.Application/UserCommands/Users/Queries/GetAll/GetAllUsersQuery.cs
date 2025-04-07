using AbySalto.Mid.Application.Abstractions.Messaging;
using AbySalto.Mid.Domain.DTOs.Responses;

namespace AbySalto.Mid.Application.UserCommands.Users.Queries.GetAll
{
    public record GetAllUsersQuery() : IQuery<ICollection<UserResponseDto?>>;
}
