using AbySalto.Mid.Application.Abstractions.Messaging;

namespace AbySalto.Mid.Application.UserCommands.Cart.Commands.AddToCart
{
    public record AddToCartCommand(int ProductId, int Quantity, int UserId) : ICommand;
}
