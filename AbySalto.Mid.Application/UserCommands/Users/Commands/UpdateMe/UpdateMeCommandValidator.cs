using AbySalto.Mid.Domain.Core.Errors;
using AbySalto.Mid.Domain.Interfaces.Services;
using FluentValidation;

namespace AbySalto.Mid.Application.UserCommands.Users.Commands.UpdateMe
{
    public sealed class UpdateMeCommandValidator : AbstractValidator<UpdateMeCommand>
    {
        public UpdateMeCommandValidator(IValidationService validationService, IPasswordService passwordService)
        {
            // Username
            RuleFor(x => x.Username)
                .NotEmpty()
                .Must((request, x) =>
                {
                    return validationService.IsUsernameUnique(request.Username, request.Id);
                }).WithMessage(DomainErrors.User.UsernameIsInUse.Message);

            // Name
            RuleFor(x => x.Name)
                .NotEmpty();

            // Password
            RuleFor(x => x.Password)
                .MinimumLength(8).When(x => !string.IsNullOrEmpty(x.Password))
                .Must((request, x) =>
                {
                    return passwordService.CheckPasswordStrength(request.Password);
                }).When(x => !string.IsNullOrEmpty(x.Password))
                .WithMessage(DomainErrors.User.InvalidCredentials.Message)
                .Must((request, x) =>
                {
                    return request.PasswordConfirm is not null ? true : false;
                }).When(x => !string.IsNullOrEmpty(x.Password))
                .WithMessage(DomainErrors.User.InvalidCredentials.Message)
                .Must((request, x) =>
                {
                    return request.Password == request.PasswordConfirm ? true : false;
                }).When(x => !string.IsNullOrEmpty(x.Password))
                .WithMessage(DomainErrors.User.InvalidCredentials.Message);
        }
    }
}
