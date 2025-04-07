using AbySalto.Mid.Domain.Interfaces.Repository;
using AbySalto.Mid.Domain.Interfaces.Services;
using PhoneNumbers;

namespace AbySalto.Mid.Infrastructure.Services
{
    public class ValidationService : IValidationService
    {
        private readonly IUserRepository _userRepository;

        public ValidationService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool IsUsernameUnique(string username, int? userId = null)
        {
            try
            {
                var user = _userRepository.IsUsernameUnique(username, userId);
                return user;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool IsUserEmailUnique(string email, int? userId = null)
        {
            try
            {
                var user = _userRepository.IsEmailUnique(email, userId);
                return user;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
