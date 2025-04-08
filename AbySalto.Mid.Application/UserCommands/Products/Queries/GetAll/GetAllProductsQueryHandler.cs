using AbySalto.Mid.Application.Abstractions.Messaging;
using AbySalto.Mid.Application.Mappers;
using AbySalto.Mid.Domain.Core.Errors;
using AbySalto.Mid.Domain.Core.Primitives;
using AbySalto.Mid.Domain.DTOs.Responses;
using AbySalto.Mid.Domain.Entities;
using AbySalto.Mid.Domain.Interfaces.Repository;
using AbySalto.Mid.Domain.Interfaces.Services;

namespace AbySalto.Mid.Application.UserCommands.Products.Queries.GetAll
{
    internal sealed class GetAllProductsQueryHandler : IQueryHandler<GetAllProductsQuery, ICollection<ProductResponseDto?>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMetaRepository _metaRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IDimensionsRepository _dimensionsRepository;
        private readonly IExternalApiService _externalApiService;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllProductsQueryHandler(IProductRepository productRepository, IExternalApiService externalApiService, IMetaRepository metaRepository, IReviewRepository reviewRepository, IDimensionsRepository dimensionsRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _externalApiService = externalApiService;
            _metaRepository = metaRepository;
            _reviewRepository = reviewRepository;
            _dimensionsRepository = dimensionsRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<ICollection<ProductResponseDto?>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken = default)
        {
            var productsCount = await _externalApiService.GetProductsCount();
            var products = await _productRepository.GetAll(null, "Dimensions,Reviews,Meta");

            if (products.Count() < 1 || products.Count() < productsCount)
            {
                products = await _externalApiService.GetProducts();

                var productsToAdd = new List<Product>();
                var metaToAdd = new List<Meta>();
                var dimensionsToAdd = new List<Dimensions>();
                var reviewsToAdd = new List<Review>();

                foreach (var product in products)
                {
                    var existingProduct = await _productRepository.GetByIdAsync((int)product.Id, cancellationToken);
                    if (existingProduct is null)
                    {
                        product.ApiId = product.Id;
                        product.Id = null;
                        // Prepare entities to be added to the database
                        productsToAdd.Add(product);
                    }
                }

                if (productsToAdd.Any())
                {
                    _productRepository.AddRange(productsToAdd);
                    await _unitOfWork.SaveChangesAsync(cancellationToken);
                }

                foreach (var product in products)
                {
                    if (product.Id is null) 
                    {
                        var existingProduct = await _productRepository.GetByApiIdAsync((int)product.ApiId, cancellationToken);

                        // Prepare related entities
                        product.Meta.ProductId = (int)existingProduct.Id;
                        metaToAdd.Add(product.Meta);

                        product.Dimensions.ProductId = (int)existingProduct.Id;
                        dimensionsToAdd.Add(product.Dimensions);

                        foreach (var review in product.Reviews)
                        {
                            review.ProductId = (int)existingProduct.Id;
                            reviewsToAdd.Add(review);
                        }
                    }
                }

                // Add all products and related entities at once
                if (metaToAdd.Any())
                {
                    _metaRepository.AddRange(metaToAdd);
                }

                if (dimensionsToAdd.Any())
                {
                    _dimensionsRepository.AddRange(dimensionsToAdd);
                }

                if (reviewsToAdd.Any())
                {
                    _reviewRepository.AddRange(reviewsToAdd);
                }

                // Commit all changes in a single transaction
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }

            if (products is null)
            {
                return Result.Failure<ICollection<ProductResponseDto?>>(
                    DomainErrors.Product.DoesNotExist);
            }

            return Result.Success(products.ToDto());
        }
    }
}
