namespace AbySalto.Mid.Domain.Enums
{
    public enum ErrorCode
    {
        // Custom errors
        UserNotFound,
        EmailInUse,
        IdPresentInCreate,
        InvalidEmailOrPassword,
        ActionForbidden,
        NotFound,
        PropertyNotFound,
        CantReauthenticate,
        PhoneNumberNotVerified,
        PhoneNumberAlreadyVerified,
        PhoneNumberAlreadyInUse,
        PhoneNumberFormatInvalid,
        InvalidOrExpiredCode,
        PasswordConfirmationNotSame,
        SyncRequestAlreadyExists,
        AlertNotFound,
        EmailNotVerified,
        PromotionNotFound,
        GeofenceNotFound,
        CodeResendTimeout,
        ChatNotFound,
        PromotionsNotFound,
        BrandImageNotFound,
        BrandImageIsActive,
        UserNotVerified,

        // Validation errors
        ValidationErrors = 10000,
        Required,
        NotValidEmailAddress,
        MinLength,
        MaxLength,
        Length,
        GreaterThanOrEqual,
        GreaterThan,
        LessThanOrEqual,
        LessThan,
        NotEqual,
        Predicate,
        Regex,
        Equal,
        ExactLength,
        Between,
        Empty,
        Invalid
    }
}
