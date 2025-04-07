using AbySalto.Mid.Domain.Enums;

namespace AbySalto.Mid.Domain.Core.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException()
        {
        }

        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(ErrorCode errorCode) : base(errorCode.ToString())
        {
        }
    }
}
