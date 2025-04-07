using AbySalto.Mid.Application.Abstractions.Messaging;

namespace AbySalto.Mid.Application.UserCommands.Users.Commands.Logout
{
    public record LogoutCommand(int Id) : ICommand;
}
