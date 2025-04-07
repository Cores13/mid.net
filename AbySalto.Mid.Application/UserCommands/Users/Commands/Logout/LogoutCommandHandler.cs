using AbySalto.Mid.Application.Abstractions.Messaging;
using AbySalto.Mid.Domain.Core.Primitives;
using AbySalto.Mid.Domain.Interfaces.Repository;
using AbySalto.Mid.Domain.Core.Errors;

namespace AbySalto.Mid.Application.UserCommands.Users.Commands.Logout
{
    internal sealed class LogoutCommandHandler : ICommandHandler<LogoutCommand>
    {
        private readonly IUserRepository _userRepository;

        public LogoutCommandHandler(
            IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result> Handle(LogoutCommand request, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);

            if (user is null)
            {
                return Result.Failure(
                    DomainErrors.User.InvalidCredentials);
            }

            user.RefreshToken = null;
            user.RefreshTokenExpiryTime = DateTime.UtcNow;


            return Result.Success();
        }
    }
}
