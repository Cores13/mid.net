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
        public static class User
        {
            /// <summary>
            /// Email or password is not valid error.
            /// </summary>
            public static Error InvalidCredentials => new Error("User.InvalidCredentials", "Email or password is not valid.");

            /// <summary>
            /// User does not exist error.
            /// </summary>
            public static Error DoesNotExist => new Error("User.DoesNotExist", "User does not exist.");
            
            /// <summary>
            /// User does not exist error.
            /// </summary>
            public static Error UsernameIsInUse => new Error("User.UsernameIsInUse", "Username already in use.");

            /// <summary>
            /// User does not exist error.
            /// </summary>
            public static Error ResetPasswordTokenDoesNotExist => new Error("User.ResetPasswordTokenDoesNotExist", "Password reset token does not exist.");
            
            /// <summary>
            /// User does not exist error.
            /// </summary>
            public static Error EmailVerificationTokenDoesNotExist => new Error("User.EmailVerificationTokenDoesNotExist", "Email verification token does not exist.");

            /// <summary>
            /// Specified role does not exist error.
            /// </summary>
            public static Error InvalidRole => new Error("User.InvalidRole", "Specified role does not exist.");

            /// <summary>
            /// Invalid request error.
            /// </summary>
            public static Error InvalidRequest => new Error("User.InvalidRequest", "Invalid request.");
            
            /// <summary>
            /// Invalid request error.
            /// </summary>
            public static Error InvalidPhoneNumber => new Error("User.InvalidPhoneNumber", "Invalid phone number.");
                 
            /// <summary>
            /// Invalid request error.
            /// </summary>
            public static Error PhoneNumberNotUnique => new Error("User.PhoneNumberNotUnique", "Phone number already in use.");
                       
            /// <summary>
            /// Invalid request error.
            /// </summary>
            public static Error PhoneNumberRegionNotFound => new Error("User.PhoneNumberRegionNotFound", "Phone number region missing.");

            /// <summary>
            /// User email is not verified error.
            /// </summary>
            public static Error UserNotVerified => new Error("User.UserNotVerified", "User email is not verified.");
            
            /// <summary>
            /// User email is not verified error.
            /// </summary>
            public static Error UserInactive => new Error("User.UserInactive", "User is inactive.");

            /// <summary>
            /// User email is not verified error.
            /// </summary>
            public static Error EmailAlreadyVerified => new Error("User.EmailAlreadyVerified", "Your email is already verified.");
        }
    }
}
