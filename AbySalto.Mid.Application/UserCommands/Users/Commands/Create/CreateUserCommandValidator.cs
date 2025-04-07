using AbySalto.Mid.Domain.Core.Errors;
using AbySalto.Mid.Domain.Interfaces.Services;
using FluentValidation;

namespace AbySalto.Mid.Application.UserCommands.Users.Commands.Create
{
    public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator(IValidationService validationService, IPasswordService passwordService)
        {
            RuleFor(x => x.Name)
                .NotEmpty();
            
            RuleFor(x => x.Role)
                .NotEmpty();
            
            RuleFor(x => x.Status)
                .NotEmpty();

            // Email
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .Must((request, x) =>
                {
                    return validationService.IsUserEmailUnique(request.Email);
                }).WithMessage(DomainErrors.Email.EmailInUse.Message);

            // Username
            RuleFor(x => x.Username)
                .NotEmpty()
                .Must((request, x) =>
                {
                    return validationService.IsUsernameUnique(request.Username);
                }).WithMessage(DomainErrors.User.UsernameIsInUse.Message);

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
