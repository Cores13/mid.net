using AbySalto.Mid.Domain.DTOs.Paging;
using AbySalto.Mid.Domain.DTOs.Responses;
using AbySalto.Mid.Domain.Entities;

namespace AbySalto.Mid.Domain.Interfaces.Repository
{
    public interface IReviewRepository : IRepository<Review>
    {
        Task<PagedResponse<ReviewResponseDto?>> GetAllPaged(PagedRequest<string> pagedQuery);

        Task<Review?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    }
}
