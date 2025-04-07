using AbySalto.Mid.Domain.DTOs.Requests;
using AbySalto.Mid.Domain.DTOs.Responses;
using AbySalto.Mid.Domain.Enums;
using AbySalto.Mid.Domain.Entities;

namespace AbySalto.Mid.Application.Mappers
{
    public static class UserMapper 
    {
        public static User ToModel(this UserRequestDto user)
        {
            return new User
            {
                Id = user.Id,
                Username = user.Username,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role is null ? null : (UserRoleEnum)Enum.ToObject(typeof(UserRoleEnum), user.Role),
                Status = (UserStatusEnum)Enum.ToObject(typeof(UserRoleEnum), user.Status)
            };
        }

        public static UserResponseDto ToDto(this User user, UserResponseDtoMapOptions mapOptions = null)
        {
            return new UserResponseDto
            {
                Id = (int)user.Id,
                Username = user.Username,
                Name = user.Name,
                Email = user.Email,
                Role = (UserRoleEnum)user.Role,
                Status = (UserStatusEnum)user.Status,
                CreatedOn = user.CreatedOn,
            };
        }

        public static ICollection<User> ToModel(this IEnumerable<UserRequestDto> users) => users.Select(x => x.ToModel()).ToList();

        public static ICollection<UserResponseDto> ToDto(this IEnumerable<User> users) => users.Select(x => x.ToDto()).ToList();
        
        public static ICollection<UserResponseDto> ToDto(this List<User> users, UserResponseDtoMapOptions mapOptions = null) => users.Select(x => x.ToDto(mapOptions)).ToList();
        
        public class UserResponseDtoMapOptions
        {
            public bool Contacts { get; set; }
        }
    }

}
