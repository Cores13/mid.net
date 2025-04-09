using AbySalto.Mid.Application.Abstractions.Messaging;

namespace AbySalto.Mid.Application.UserCommands.Cart.Commands.RemoveFromCart
{
    public record RemoveFromCartCommand(int ProductId, int Quantity, int UserId) : ICommand;
}
