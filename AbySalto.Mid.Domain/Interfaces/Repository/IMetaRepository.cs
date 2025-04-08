using AbySalto.Mid.Domain.DTOs.Paging;
using AbySalto.Mid.Domain.DTOs.Responses;
using AbySalto.Mid.Domain.Entities;

namespace AbySalto.Mid.Domain.Interfaces.Repository
{
    public interface IMetaRepository : IRepository<Meta>
    {
        Task<PagedResponse<MetaResponseDto?>> GetAllPaged(PagedRequest<string> pagedQuery);

        Task<Meta?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    }
}
