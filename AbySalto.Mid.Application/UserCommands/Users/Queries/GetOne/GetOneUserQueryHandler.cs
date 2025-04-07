using AbySalto.Mid.Application.Abstractions.Messaging;
using AbySalto.Mid.Application.Mappers;
using AbySalto.Mid.Domain.Core.Errors;
using AbySalto.Mid.Domain.Core.Primitives;
using AbySalto.Mid.Domain.DTOs.Responses;
using AbySalto.Mid.Domain.Interfaces.Repository;

namespace AbySalto.Mid.Application.UserCommands.Users.Queries.GetOne
{
    internal sealed class GetOneUserQueryHandler : IQueryHandler<GetOneUserQuery, UserResponseDto>
    {
        private readonly IUserRepository _userRepository;

        public GetOneUserQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<UserResponseDto>> Handle(GetOneUserQuery request, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);

            if (user is null)
            {
                return Result.Failure<UserResponseDto>(
                    DomainErrors.User.DoesNotExist);
            }

            var userDto = user.ToDto();

            return Result.Success(userDto);
        }
    }
}
