namespace AbySalto.Mid.Domain.DTOs.Requests
{
    public class UserRequestDto
    {
        public int? Id { get; set; }

        public string Username { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string? Password { get; set; }

        public string? PasswordConfirm { get; set; }

        public int? Role { get; set; }

        public int? Status { get; set; }
    }
}
