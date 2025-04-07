using AbySalto.Mid.Application.Abstractions.Messaging;

namespace AbySalto.Mid.Application.UserCommands.Password.ForgotPasswordReset
{
    public record ForgotPasswordResetCommand(string Email, string Password, string Code) : ICommand;
}
