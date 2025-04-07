using FluentValidation;

namespace AbySalto.Mid.Domain.DTOs.Request
{
    public class ForgotPasswordResetDto : ForgotPasswordVerifyCodeDto
    {
        public string Password { get; set; }
    }

    public class ForgotPasswordResetDtoValidator : AbstractValidator<ForgotPasswordResetDto>
    {
        public ForgotPasswordResetDtoValidator()
        {
            RuleFor(x => x.Code)
              .NotEmpty()
              .MinimumLength(4);

            RuleFor(x => x.Password)
                .MinimumLength(6)
                .NotEmpty();
        }
    }
}
