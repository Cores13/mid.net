using FluentValidation;

namespace AbySalto.Mid.Application.UserCommands.Password.ForgotPasswordReset
{
    public sealed class ForgotPasswordResetCommandValidator : AbstractValidator<ForgotPasswordResetCommand>
    {
        public ForgotPasswordResetCommandValidator()
        {
            RuleFor(x => x.Code)
              .NotEmpty()
              .MinimumLength(12);

            RuleFor(x => x.Password)
                .MinimumLength(6)
                .NotEmpty();
        }
    }
}
