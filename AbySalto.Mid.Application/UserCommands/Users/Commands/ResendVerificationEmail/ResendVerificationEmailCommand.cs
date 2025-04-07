using AbySalto.Mid.Application.Abstractions.Messaging;

namespace AbySalto.Mid.Application.UserCommands.Users.Commands.ResendVerificationEmail
{
    public record ResendVerificationEmailCommand(int Id) : ICommand;
}
