using AbySalto.Mid.Application.Abstractions.Messaging;

namespace AbySalto.Mid.Application.UserCommands.Products.Commands.AddToFavorites
{
    public record AddProductToFavoritesCommand(int Id, int UserId) : ICommand;
}
