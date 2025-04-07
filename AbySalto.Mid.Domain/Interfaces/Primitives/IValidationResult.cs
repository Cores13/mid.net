using AbySalto.Mid.Domain.Core.Primitives;

namespace AbySalto.Mid.Domain.Interfaces.Primitives
{
    public interface IValidationResult
    {
        public static readonly Error ValidationError = new(
            "ValidationError",
            "A validation problem occurred.");

        Error[] Errors { get; }
    }
}
