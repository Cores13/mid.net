using AbySalto.Mid.Domain.Enums;
using System.Text.Json.Serialization;

namespace AbySalto.Mid.Domain.DTOs.Responses
{
    public class UserResponseDto
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public UserRoleEnum Role { get; set; }

        public UserStatusEnum Status { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
