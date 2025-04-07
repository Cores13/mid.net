using AbySalto.Mid.Application.Abstractions.Messaging;
using AbySalto.Mid.Domain.Core.Errors;
using AbySalto.Mid.Domain.Core.Primitives;
using AbySalto.Mid.Domain.DTOs.Paging;
using AbySalto.Mid.Domain.DTOs.Responses;
using AbySalto.Mid.Domain.Interfaces.Repository;

namespace AbySalto.Mid.Application.UserCommands.Users.Queries.GetAllPaginated
{
    internal sealed class GetAllUsersPaginatedQueryHandler : IQueryHandler<GetAllUsersPaginatedQuery, PagedResponse<UserResponseDto?>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllUsersPaginatedQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<PagedResponse<UserResponseDto?>>> Handle(GetAllUsersPaginatedQuery request, CancellationToken cancellationToken = default)
        {
            var users = await _userRepository.GetAllPaged(request.PagedQuery);

            if (users is null)
            {
                return Result.Failure<PagedResponse<UserResponseDto?>>(
                    DomainErrors.User.DoesNotExist);
            }

            return Result.Success(users);
        }
    }
}
