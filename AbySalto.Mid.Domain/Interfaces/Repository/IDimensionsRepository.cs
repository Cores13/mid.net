using AbySalto.Mid.Domain.DTOs.Paging;
using AbySalto.Mid.Domain.DTOs.Responses;
using AbySalto.Mid.Domain.Entities;

namespace AbySalto.Mid.Domain.Interfaces.Repository
{
    public interface IDimensionsRepository : IRepository<Dimensions>
    {
        Task<PagedResponse<DimensionsResponseDto?>> GetAllPaged(PagedRequest<string> pagedQuery);

        Task<Dimensions?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    }
}
