namespace AbySalto.Mid.Domain.Interfaces.Services
{
    public interface IValidationService
    {
        bool IsUsernameUnique(string username, int? userId = null);

        bool IsUserEmailUnique(string email, int? userId = null);
    }
}
