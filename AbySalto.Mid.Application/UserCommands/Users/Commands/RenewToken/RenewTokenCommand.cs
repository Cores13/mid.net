using AbySalto.Mid.Application.Abstractions.Messaging;
using AbySalto.Mid.Domain.DTOs.Responses;

namespace AbySalto.Mid.Application.UserCommands.Users.Commands.RenewToken
{
    public record RenewTokenCommand(string RefreshToken) : ICommand<TokenResponseDto>;
}
