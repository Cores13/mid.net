using AbySalto.Mid.Domain.DTOs.Responses;
using AbySalto.Mid.Domain.Entities;
using AbySalto.Mid.Domain.Interfaces.Repository;
using AbySalto.Mid.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using AbySalto.Mid.Application.Mappers;
using AbySalto.Mid.Domain.DTOs.Paging;

namespace AbySalto.Mid.Infrastructure.Repository
{
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        private readonly ApplicationDbContext _context;

        public CartRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PagedResponse<CartResponseDto?>> GetAllPaged(PagedRequest<string> pagedQuery)
        {
            var query = _context.Carts
                .AsQueryable();

            var totalResults = await query.CountAsync();

            query = query.Skip(pagedQuery.ItemsToSkip()).Take(pagedQuery.PageSize);

            var results = await query.ToListAsync();

            return new PagedResponse<CartResponseDto?>
            {
                Results = results.ToDto(),
                Page = pagedQuery.Page,
                PageSize = pagedQuery.PageSize,
                TotalResults = totalResults
            };
        }

        public async Task<Cart?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Carts
                .Include(x => x.Products)
                    .ThenInclude(x => x.Product)
                .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<Cart?> GetByUserIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Carts
                .Include(x => x.Products)
                    .ThenInclude(x => x.Product)
                .SingleOrDefaultAsync(x => x.UserId == id, cancellationToken);
        }

        public void AddToCart(CartProduct cartItem)
        {
            _context.CartProducts.Add(cartItem);
        }

        public void RemoveFromCart(CartProduct cartItem)
        {
            _context.CartProducts.Remove(cartItem);
        }

        public async Task<CartProduct?> GetProductFromCart(int productId, int cartId, CancellationToken cancellationToken = default)
        {
            return await _context.CartProducts.SingleOrDefaultAsync(x => x.CartId == cartId && x.ProductId == productId, cancellationToken);
        }
    }
}
