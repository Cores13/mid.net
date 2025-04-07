using AbySalto.Mid.Application.Abstractions.Messaging;
using AbySalto.Mid.Domain.Core.Primitives;
using AbySalto.Mid.Domain.Interfaces.Repository;

namespace AbySalto.Mid.Application.UserCommands.Users.Commands.Delete
{
    public sealed class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.GetByIdAsync((int)request.Id, cancellationToken);
            _userRepository.Remove(user);

            return Result.Success();
        }
    }
}
