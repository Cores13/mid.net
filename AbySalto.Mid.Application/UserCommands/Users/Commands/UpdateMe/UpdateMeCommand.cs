using AbySalto.Mid.Application.Abstractions.Messaging;

namespace AbySalto.Mid.Application.UserCommands.Users.Commands.UpdateMe
{
    public record UpdateMeCommand(
        int? Id,
        string Username,
        string Name,
        string? Password,
        string? PasswordConfirm) : ICommand;
}
