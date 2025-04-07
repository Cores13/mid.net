using AbySalto.Mid.Domain.DTOs.Responses;
using AbySalto.Mid.Domain.Entities;
using AbySalto.Mid.Domain.Interfaces.Repository;
using AbySalto.Mid.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using AbySalto.Mid.Application.Mappers;
using AbySalto.Mid.Domain.DTOs.Paging;
using Newtonsoft.Json;
using AbySalto.Mid.Domain.Enums;
using AbySalto.Mid.Domain.Core.Extensions;

namespace AbySalto.Mid.Infrastructure.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PagedResponse<UserResponseDto?>> GetAllPaged(PagedRequest<string> pagedQuery)
        {
            var query = _context.Users
                .AsQueryable();

            if (pagedQuery.Filter != null)
            {
                var filters = JsonConvert.DeserializeObject<Dictionary<string, object>>(pagedQuery.Filter);

                if (filters.TryGetValue("search", out var nameFilter) && !string.IsNullOrWhiteSpace(nameFilter.ToString()))
                {
                    query = query.Where(x => x.Name.Contains(nameFilter.ToString()) || x.Email.Contains(nameFilter.ToString()));
                }

                if (filters.TryGetValue("role", out var roleFilter) && roleFilter != null)
                {
                    var role = (UserRoleEnum)Enum.ToObject(typeof(UserRoleEnum), roleFilter);
                    query = query.Where(x => x.Role == role);
                }

                if (filters.TryGetValue("status", out var statusFilter) && statusFilter != null)
                {
                    var status = (UserStatusEnum)Enum.ToObject(typeof(UserStatusEnum), statusFilter);
                    query = query.Where(x => x.Status == status);
                }
            }

            if (!string.IsNullOrWhiteSpace(pagedQuery.OrderByKey))
            {
                query = query.OrderBy(pagedQuery.OrderByKey.ToString(), !pagedQuery.IsDescending);
            }

            var totalResults = await query.CountAsync();

            query = query.Skip(pagedQuery.ItemsToSkip()).Take(pagedQuery.PageSize);

            var results = await query.ToListAsync();

            var mapOptions = new UserMapper.UserResponseDtoMapOptions
            {
                Contacts = true
            };

            return new PagedResponse<UserResponseDto?>
            {
                Results = results.ToDto(mapOptions),
                Page = pagedQuery.Page,
                PageSize = pagedQuery.PageSize,
                TotalResults = totalResults
            };
        }

        public async Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Users.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<User?> GetByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
        {
            return await _context.Users.SingleOrDefaultAsync(x => x.RefreshToken == refreshToken, cancellationToken);
        }

        public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return await _context.Users.SingleOrDefaultAsync(x => x.Email == email, cancellationToken);
        }

        public async Task Update(User entity, CancellationToken cancellationToken = default)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Id == entity.Id, cancellationToken);
            user.Username = string.IsNullOrEmpty(entity.Username) ? user.Username : entity.Username;
            user.Name = string.IsNullOrEmpty(entity.Name) ? user.Name : entity.Name;
            user.Email = string.IsNullOrEmpty(entity.Email) ? user.Email : entity.Email;
            user.PasswordHash = entity.PasswordHash ?? user.PasswordHash;
            user.PasswordSalt = entity.PasswordSalt ?? user.PasswordSalt;
            user.Role = entity.Role is null ? user.Role : (UserRoleEnum)Enum.ToObject(typeof(UserRoleEnum), entity.Role);
            user.Status = entity.Status is null ? user.Status : (UserStatusEnum)Enum.ToObject(typeof(UserRoleEnum), entity.Status);
            user.UpdatedOn = DateTime.UtcNow;

        }

        public bool IsEmailUnique(string email, int ? id = null)
        {
            var user = _context.Users.FirstOrDefault(x => x.Email == email &&  x.Id != id);

            return user is null ? true : false;
        }

        public bool IsUsernameUnique(string username, int ? id = null)
        {
            var user = _context.Users.FirstOrDefault(x => x.Username == username &&  x.Id != id);

            return user is null ? true : false;
        }
    }
}
