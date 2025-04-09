using AbySalto.Mid.Domain.DTOs.Responses;
using AbySalto.Mid.Domain.Entities;
using AbySalto.Mid.Domain.Interfaces.Repository;
using AbySalto.Mid.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using AbySalto.Mid.Application.Mappers;
using AbySalto.Mid.Domain.DTOs.Paging;

namespace AbySalto.Mid.Infrastructure.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PagedResponse<ProductResponseDto?>> GetAllPaged(PagedRequest<string> pagedQuery)
        {
            var query = _context.Products
                .Include(x => x.Dimensions)
                .Include(x => x.Reviews)
                .Include(x => x.Meta)
                .AsQueryable();

            var totalResults = await query.CountAsync();

            query = query.Skip(pagedQuery.ItemsToSkip()).Take(pagedQuery.PageSize);

            var results = await query.ToListAsync();

            return new PagedResponse<ProductResponseDto?>
            {
                Results = results.ToDto(),
                Page = pagedQuery.Page,
                PageSize = pagedQuery.PageSize,
                TotalResults = totalResults
            };
        }

        public async Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Products
                .Include("Dimensions")
                .Include("Reviews")
                .Include("Meta")
                .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<Product?> GetByApiIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Products
                .Include("Dimensions")
                .Include("Reviews")
                .Include("Meta")
                .SingleOrDefaultAsync(x => x.ApiId == id, cancellationToken);
        }

        public void AddToFavorites(UserFavorite entity)
        {
            _context.UserFavorites.Add(entity);
        }
    }
}
