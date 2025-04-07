using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using AbySalto.Mid.Domain.Entities;
using AbySalto.Mid.Domain.Interfaces.Services;

namespace AbySalto.Mid.Infrastructure.Services
{
    public class PasswordService : IPasswordService
    {
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be empty or whitespace.", "password");

            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public bool VerifyPassword(User user, string password)
        {
            if (user.PasswordHash.Length != 64)
                throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");

            if (user.PasswordSalt.Length != 128)
                throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            //for (int i = 0; i < user.PasswordSalt.Length; i++)
            //{
            //    Console.Write(user.PasswordSalt[i] + ", ");
            //}
            using (var hmac = new HMACSHA512(user.PasswordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    //Console.Write(user.PasswordHash[i] + ", ");

                    if (computedHash[i] != user.PasswordHash[i]) return false;
                }
            }

            return true;
        }

        public bool CheckPasswordStrength(string password)
        {
            //StringBuilder stringBuilder = new StringBuilder();
            if (password.Length < 8)
            {
                //stringBuilder.Append("Minimum password length should be 8 characters." + Environment.NewLine);
                return false;
            }
            if (!(Regex.IsMatch(password, "[a-z]") && Regex.IsMatch(password, "[A-Z]") && Regex.IsMatch(password, "[0-9]")))
            {
                //stringBuilder.Append("Password should be alphanumeric." + Environment.NewLine);
                return false;
            }
            if (!Regex.IsMatch(password, "[`~,!,@,#,$,%,^,&,*,(,),+,=,{,},/,|,:,;,\\,\\[,\\],|,',<,>,.,?,_,]"))
            {
                //stringBuilder.Append("Password should contain special characters." + Environment.NewLine);
                return false;
            }
            //return stringBuilder.ToString();
            return true;
        }
    }
}
