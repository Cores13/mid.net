using AbySalto.Mid.Domain.Enums;
using AbySalto.Mid.Domain.Entities;

namespace AbySalto.Mid.Domain.Seeders
{
    public class UserSeeder
    {
        // Matrix2023!
        private static byte[] passwordHash = new byte[] { 35, 90, 244, 18, 43, 21, 195, 38, 253, 159, 107, 4, 55, 18, 200, 156, 84, 209, 241, 56, 27, 72, 90, 53, 220, 177, 39, 238, 117, 222, 23, 141, 154, 189, 55, 192, 113, 129, 205, 207, 226, 181, 138, 80, 84, 166, 226, 153, 64, 24, 39, 131, 91, 56, 154, 150, 210, 129, 163, 197, 230, 210, 1, 184 };
        private static byte[] passwordSalt = new byte[] { 141, 249, 58, 0, 174, 228, 48, 60, 70, 120, 31, 142, 153, 188, 235, 210, 202, 154, 79, 198, 74, 194, 78, 154, 133, 112, 30, 189, 171, 202, 164, 9, 106, 78, 229, 131, 106, 18, 44, 114, 165, 89, 98, 17, 156, 31, 248, 142, 103, 98, 52, 176, 27, 154, 19, 178, 8, 31, 81, 183, 114, 237, 44, 80, 213, 69, 239, 33, 27, 95, 113, 116, 150, 169, 203, 208, 49, 84, 57, 29, 132, 148, 75, 131, 223, 59, 207, 147, 37, 91, 50, 190, 55, 204, 95, 113, 39, 201, 179, 191, 245, 184, 61, 146, 8, 47, 145, 224, 10, 240, 216, 97, 159, 135, 199, 201, 113, 50, 57, 45, 232, 72, 16, 138, 86, 166, 101, 64 };


        public static User[] Data =
        {
            new User()
            {
                Id = 1,
                Username = "Administrator",
                Name = "Administrator",
                Email = "admin@authentication.com",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                EmailVerifiedAt = DateTime.UtcNow,
                RefreshToken = null,
                RefreshTokenExpiryTime = DateTime.UtcNow,
                ResetPasswordToken = null,
                ResetPasswordExpiry = DateTime.UtcNow,
                Role = UserRoleEnum.Administrator,
                Status = UserStatusEnum.Active,
            },

            new User()
            {
                Id = 2,
                Username = "User",
                Name = "User",
                Email = "user@authentication.com",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                EmailVerifiedAt = DateTime.UtcNow,
                RefreshToken = null,
                RefreshTokenExpiryTime = DateTime.UtcNow,
                ResetPasswordToken = null,
                ResetPasswordExpiry = DateTime.UtcNow,
                Role = UserRoleEnum.User,
                Status = UserStatusEnum.Active,
            }
        };
    }
}