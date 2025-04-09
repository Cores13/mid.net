using AbySalto.Mid.Application.Abstractions.Messaging;

namespace AbySalto.Mid.Application.UserCommands.Products.Commands.Create
{
    public record AddProductToFavoritesCommand(int Id, int UserId) : ICommand;
}
