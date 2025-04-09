using AbySalto.Mid.Application.Abstractions.Messaging;
using AbySalto.Mid.Application.UserCommands.Products.Commands.Create;
using AbySalto.Mid.Domain.Core.Errors;
using AbySalto.Mid.Domain.Core.Primitives;
using AbySalto.Mid.Domain.DTOs.Responses;
using AbySalto.Mid.Domain.Entities;
using AbySalto.Mid.Domain.Interfaces.Repository;
using AbySalto.Mid.Domain.Interfaces.Services;

namespace AbySalto.Mid.Application.UserCommands.Users.Commands.Create
{
    internal sealed class AddProductToFavoritesCommandHandler : ICommandHandler<AddProductToFavoritesCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly IExternalApiService _externalApiService;

        public AddProductToFavoritesCommandHandler(IProductRepository productRepository, IExternalApiService externalApiService)
        {
            _productRepository = productRepository;
            _externalApiService = externalApiService;
        }

        public async Task<Result> Handle(AddProductToFavoritesCommand request, CancellationToken cancellationToken = default)
        {
            var product = await _productRepository.GetByApiIdAsync(request.Id);

            if (product is null)
            {
                product = await _externalApiService.GetProductById(request.Id);
                if (product is null)
                {
                    return Result.Failure<ProductResponseDto?>(
                        DomainErrors.Product.DoesNotExist);
                }
                product.ApiId = product.Id;
                product.Id = null;

                _productRepository.Add(product);
            }

            if (product is null)
            {
                return Result.Failure<ProductResponseDto?>(
                    DomainErrors.Product.DoesNotExist);
            }

            var userFavorite = new UserFavorite
            {
                UserId = request.UserId,
                ProductId = request.Id
            };

            _productRepository.AddToFavorites(userFavorite);

            return Result.Success();
        }
    }
}
