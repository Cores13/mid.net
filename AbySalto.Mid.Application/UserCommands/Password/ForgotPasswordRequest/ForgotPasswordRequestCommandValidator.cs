using FluentValidation;

namespace AbySalto.Mid.Application.UserCommands.Password.ForgotPasswordRequest
{
    public sealed class ForgotPasswordRequestCommandValidator : AbstractValidator<ForgotPasswordRequestCommand>
    {
        public ForgotPasswordRequestCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty();
        }
    }
}
