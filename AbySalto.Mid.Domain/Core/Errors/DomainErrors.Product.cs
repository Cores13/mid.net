using AbySalto.Mid.Domain.Core.Primitives;

namespace AbySalto.Mid.Domain.Core.Errors
{
    /// <summary>
    /// Contains the domain errors.
    /// </summary>
    public static partial class DomainErrors
    {
        /// <summary>
        /// Contains the model name errors.
        /// </summary>
        public static class Product
        {
            /// <summary>
            /// Product does not exist error.
            /// </summary>
            public static Error DoesNotExist => new Error("Product.DoesNotExist", "Product does not exist.");

        }
    }
}
