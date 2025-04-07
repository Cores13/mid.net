using AbySalto.Mid.Domain.Enums;

namespace AbySalto.Mid.Domain.Core.Exceptions
{
    public class ForbiddenException : Exception
    {
        public ForbiddenException(ErrorCode errorCode = ErrorCode.ActionForbidden) : base(errorCode.ToString())
        {
        }
    }
}
