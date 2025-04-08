using AbySalto.Mid.Domain.Entities;

namespace AbySalto.Mid.Domain.Interfaces.Services
{
    public interface IExternalApiService
    {
        Task<List<Product>> GetProducts();
        Task<int> GetProductsCount();
    }
}
