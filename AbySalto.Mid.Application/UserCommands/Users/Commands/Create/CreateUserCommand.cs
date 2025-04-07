using AbySalto.Mid.Application.Abstractions.Messaging;

namespace AbySalto.Mid.Application.UserCommands.Users.Commands.Create
{
    public record CreateUserCommand(
        string Username,
        string Name,
        string Email,
        string Password,
        string PasswordConfirm,
        int Role,
        int Status,
        int? Id) : ICommand;
}
