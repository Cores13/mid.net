using AbySalto.Mid.Application.Abstractions.Messaging;
using AbySalto.Mid.Application.Mappers;
using AbySalto.Mid.Domain.Core.Errors;
using AbySalto.Mid.Domain.Core.Primitives;
using AbySalto.Mid.Domain.DTOs.Requests;
using AbySalto.Mid.Domain.Enums;
using AbySalto.Mid.Domain.Interfaces.Repository;
using AbySalto.Mid.Domain.Interfaces.Services;

namespace AbySalto.Mid.Application.UserCommands.Users.Commands.Create
{
    internal sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordService _passwordService;
        private readonly IVerificationCodeService _verificationCodeService;

        public CreateUserCommandHandler(IUserRepository userRepository, IPasswordService passwordService, IVerificationCodeService verificationCodeService)
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
            _verificationCodeService = verificationCodeService;
        }

        public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken = default)
        {
            if (!Enum.IsDefined(typeof(UserRoleEnum), request.Role))
            {
                return Result.Failure(
                    DomainErrors.User.InvalidRole);
            }

            var user = new UserRequestDto()
            {
                Username = request.Username,
                Name = request.Name,
                Email = request.Email,
                Password = request.Password,
                Role = request.Role,
                Status = request.Status
            };
            var newUser = user.ToModel();

            byte[] passwordHash, passwordSalt;
            _passwordService.CreatePasswordHash(user.Password, out passwordHash, out passwordSalt);

            newUser.PasswordHash = passwordHash;
            newUser.PasswordSalt = passwordSalt;

            _userRepository.Add(newUser);
            _userRepository.GetAddedEntity(newUser);

            await _verificationCodeService.CreateCodeAsync((int)newUser.Id, VerificationCodeType.EmailVerification, cancellationToken);

            return Result.Success();
        }
    }
}
