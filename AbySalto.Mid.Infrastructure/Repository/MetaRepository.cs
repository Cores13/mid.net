using AbySalto.Mid.Domain.DTOs.Responses;
using AbySalto.Mid.Domain.Entities;
using AbySalto.Mid.Domain.Interfaces.Repository;
using AbySalto.Mid.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using AbySalto.Mid.Application.Mappers;
using AbySalto.Mid.Domain.DTOs.Paging;

namespace AbySalto.Mid.Infrastructure.Repository
{
    public class MetaRepository : Repository<Meta>, IMetaRepository
    {
        private readonly ApplicationDbContext _context;

        public MetaRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PagedResponse<MetaResponseDto?>> GetAllPaged(PagedRequest<string> pagedQuery)
        {
            var query = _context.Metas
                .AsQueryable();

            var totalResults = await query.CountAsync();

            query = query.Skip(pagedQuery.ItemsToSkip()).Take(pagedQuery.PageSize);

            var results = await query.ToListAsync();

            return new PagedResponse<MetaResponseDto?>
            {
                Results = results.ToDto(),
                Page = pagedQuery.Page,
                PageSize = pagedQuery.PageSize,
                TotalResults = totalResults
            };
        }

        public async Task<Meta?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Metas.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
    }
}
