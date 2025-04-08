using AbySalto.Mid.Domain.DTOs.Paging;
using AbySalto.Mid.Domain.DTOs.Responses;
using AbySalto.Mid.Domain.Entities;

namespace AbySalto.Mid.Domain.Interfaces.Repository
{
    public interface ICartRepository : IRepository<Cart>
    {
        Task<PagedResponse<CartResponseDto?>> GetAllPaged(PagedRequest<string> pagedQuery);

        Task<Cart?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    }
}
