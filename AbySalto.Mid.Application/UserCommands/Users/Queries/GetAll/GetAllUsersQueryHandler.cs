using AbySalto.Mid.Application.Abstractions.Messaging;
using AbySalto.Mid.Application.Mappers;
using AbySalto.Mid.Domain.Core.Errors;
using AbySalto.Mid.Domain.Core.Primitives;
using AbySalto.Mid.Domain.DTOs.Responses;
using AbySalto.Mid.Domain.Interfaces.Repository;

namespace AbySalto.Mid.Application.UserCommands.Users.Queries.GetAll
{
    internal sealed class GetAllUsersQueryHandler : IQueryHandler<GetAllUsersQuery, ICollection<UserResponseDto?>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<ICollection<UserResponseDto?>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken = default)
        {
            var users = await _userRepository.GetAll();

            if (users is null)
            {
                return Result.Failure<ICollection<UserResponseDto?>>(
                    DomainErrors.User.DoesNotExist);
            }

            return Result.Success(users.ToDto());
        }
    }
}
