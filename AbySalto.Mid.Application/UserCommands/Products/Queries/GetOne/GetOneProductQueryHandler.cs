using AbySalto.Mid.Application.Abstractions.Messaging;
using AbySalto.Mid.Application.Mappers;
using AbySalto.Mid.Application.UserCommands.Products.Queries.GetOne;
using AbySalto.Mid.Domain.Core.Errors;
using AbySalto.Mid.Domain.Core.Primitives;
using AbySalto.Mid.Domain.DTOs.Responses;
using AbySalto.Mid.Domain.Interfaces.Repository;
using AbySalto.Mid.Domain.Interfaces.Services;

namespace AbySalto.Mid.Application.UserCommands.Products.Queries.GetAll
{
    internal sealed class GetOneProductQueryHandler : IQueryHandler<GetOneProductQuery, ProductResponseDto?>
    {
        private readonly IProductRepository _productRepository;
        private readonly IExternalApiService _externalApiService;

        public GetOneProductQueryHandler(IProductRepository productRepository, IExternalApiService externalApiService)
        {
            _productRepository = productRepository;
            _externalApiService = externalApiService;
        }

        public async Task<Result<ProductResponseDto?>> Handle(GetOneProductQuery request, CancellationToken cancellationToken = default)
        {
            var product = await _productRepository.GetByApiIdAsync(request.Id, cancellationToken);

            if (product is null)
            {
                product = await _externalApiService.GetProductById(request.Id);
                product.ApiId = product.Id;
                product.Id = null;

                if (product is null)
                {
                    return Result.Failure<ProductResponseDto?>(
                        DomainErrors.Product.DoesNotExist);
                }

                _productRepository.Add(product);
            }

            if (product is null)
            {
                return Result.Failure<ProductResponseDto?>(
                    DomainErrors.Product.DoesNotExist);
            }

            return Result.Success(product.ToDto());
        }
    }
}
