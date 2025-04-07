using AbySalto.Mid.Application.Abstractions.Messaging;
using AbySalto.Mid.Application.Mappers;
using AbySalto.Mid.Domain.Core.Errors;
using AbySalto.Mid.Domain.Core.Primitives;
using AbySalto.Mid.Domain.DTOs.Responses;
using AbySalto.Mid.Domain.Interfaces.Repository;

namespace AbySalto.Mid.Application.UserCommands.Products.Queries.GetAll
{
    internal sealed class GetAllProductsQueryHandler : IQueryHandler<GetAllProductsQuery, ICollection<ProductResponseDto?>>
    {
        private readonly IProductRepository _productRepository;

        public GetAllProductsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Result<ICollection<ProductResponseDto?>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken = default)
        {
            var products = await _productRepository.GetAll();

            if (products is null)
            {
                return Result.Failure<ICollection<ProductResponseDto?>>(
                    DomainErrors.Product.DoesNotExist);
            }

            return Result.Success(products.ToDto());
        }
    }
}
