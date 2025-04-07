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
        public static class Email
        {
            /// <summary>
            /// Email or password is not valid error.
            /// </summary>
            public static Error EmailInUse => new Error("Email.EmailInUse", "Email is already in use.");
        }
    }
}
