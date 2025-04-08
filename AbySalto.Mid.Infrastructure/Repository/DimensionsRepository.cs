using AbySalto.Mid.Domain.DTOs.Responses;
using AbySalto.Mid.Domain.Entities;
using AbySalto.Mid.Domain.Interfaces.Repository;
using AbySalto.Mid.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using AbySalto.Mid.Application.Mappers;
using AbySalto.Mid.Domain.DTOs.Paging;

namespace AbySalto.Mid.Infrastructure.Repository
{
    public class DimensionsRepository : Repository<Dimensions>, IDimensionsRepository
    {
        private readonly ApplicationDbContext _context;

        public DimensionsRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PagedResponse<DimensionsResponseDto?>> GetAllPaged(PagedRequest<string> pagedQuery)
        {
            var query = _context.Dimensions
                .AsQueryable();

            var totalResults = await query.CountAsync();

            query = query.Skip(pagedQuery.ItemsToSkip()).Take(pagedQuery.PageSize);

            var results = await query.ToListAsync();

            return new PagedResponse<DimensionsResponseDto?>
            {
                Results = results.ToDto(),
                Page = pagedQuery.Page,
                PageSize = pagedQuery.PageSize,
                TotalResults = totalResults
            };
        }

        public async Task<Dimensions?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Dimensions.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
    }
}
