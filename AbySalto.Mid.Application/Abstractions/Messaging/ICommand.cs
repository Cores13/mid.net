using AbySalto.Mid.Domain.Core.Primitives;
using MediatR;

namespace AbySalto.Mid.Application.Abstractions.Messaging
{
    /// <summary>
    /// Represents the command interface without return type.
    /// </summary>
    public interface ICommand : IRequest<Result>
    {
    }
    /// <summary>
    /// Represents the command interface with return type.
    /// </summary>
    /// <typeparam name="TResponse">The command response type.</typeparam>
    public interface ICommand<TResponse> : IRequest<Result<TResponse>>
    {
    }
}