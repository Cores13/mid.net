namespace AbySalto.Mid.Domain.DTOs.Email
{
    public class VerificationCodeEmailDto
    {
        public required string UserName { get; set; }
        public required string Code { get; set; }
    }
}
