using AbySalto.Mid.Domain.Entities;

namespace AbySalto.Mid.Domain.Interfaces.Services
{
    public interface IPasswordService
    {
        void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        bool VerifyPassword(User user, string password);
        bool CheckPasswordStrength(string password);
    }
}
