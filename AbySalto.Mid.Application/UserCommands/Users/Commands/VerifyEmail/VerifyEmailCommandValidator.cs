using FluentValidation;

namespace AbySalto.Mid.Application.UserCommands.Users.Commands.VerifyEmail
{
    public sealed class VerifyEmailCommandValidator : AbstractValidator<VerifyEmailCommand>
    {
        public VerifyEmailCommandValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty()
                .MinimumLength(4);
        }
    }
}
