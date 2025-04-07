using AbySalto.Mid.Application.Abstractions.Messaging;

namespace AbySalto.Mid.Application.UserCommands.Users.Commands.Update
{
    public record UpdateUserCommand(
        int? Id,
        string Username,
        string Name,
        string Email,
        string? Password,
        string? PasswordConfirm,
        int? Role,
        int? Status) : ICommand;
}
