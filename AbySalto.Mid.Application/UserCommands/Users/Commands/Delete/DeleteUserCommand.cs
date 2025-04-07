using AbySalto.Mid.Application.Abstractions.Messaging;

namespace AbySalto.Mid.Application.UserCommands.Users.Commands.Delete
{
    public record DeleteUserCommand(int? Id) : ICommand;
}
