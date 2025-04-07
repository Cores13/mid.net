using FluentValidation;

namespace AbySalto.Mid.Domain.DTOs.Request
{
    public class ForgotPasswordVerifyCodeDto : ForgotPasswordRequestDto
    {
        public string Code { get; set; }
    }

    public class ForgotPasswordVerifyCodeDtoValidator : AbstractValidator<ForgotPasswordVerifyCodeDto>
    {
        public ForgotPasswordVerifyCodeDtoValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty()
                .MinimumLength(4);
        }
    }
}