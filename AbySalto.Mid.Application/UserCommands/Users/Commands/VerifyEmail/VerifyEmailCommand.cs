using AbySalto.Mid.Application.Abstractions.Messaging;

namespace AbySalto.Mid.Application.UserCommands.Users.Commands.VerifyEmail
{
    public record VerifyEmailCommand(
        int Id,
        string Code) : ICommand;
}
