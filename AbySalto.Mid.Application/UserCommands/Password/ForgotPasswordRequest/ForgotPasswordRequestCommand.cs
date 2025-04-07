using AbySalto.Mid.Application.Abstractions.Messaging;

namespace AbySalto.Mid.Application.UserCommands.Password.ForgotPasswordRequest
{
    public record ForgotPasswordRequestCommand(string Email) : ICommand;
}
