using AbySalto.Mid.Application.Abstractions.Messaging;

namespace AbySalto.Mid.Application.UserCommands.Password.ForgotPasswordVerifyCode
{
    public record ForgotPasswordVerifyCodeCommand(string Email, string Code) : ICommand;
}
