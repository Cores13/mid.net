using AbySalto.Mid.Application.Abstractions.Messaging;
using AbySalto.Mid.Domain.DTOs.Responses;

namespace AbySalto.Mid.Application.UserCommands.Users.Commands.Login
{
    public record LoginCommand(string Email, string Password) : ICommand<TokenResponseDto>;
}
