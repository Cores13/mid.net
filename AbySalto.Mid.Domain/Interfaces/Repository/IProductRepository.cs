using AbySalto.Mid.Domain.DTOs.Paging;
using AbySalto.Mid.Domain.DTOs.Responses;
using AbySalto.Mid.Domain.Entities;

namespace AbySalto.Mid.Domain.Interfaces.Repository
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<PagedResponse<ProductResponseDto?>> GetAllPaged(PagedRequest<string> pagedQuery);

        Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

        Task<Product?> GetByApiIdAsync(int id, CancellationToken cancellationToken = default);

        void AddToFavorites(UserFavorite entity);
    }
}
