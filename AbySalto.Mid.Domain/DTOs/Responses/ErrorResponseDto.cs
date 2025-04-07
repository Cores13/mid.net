using System.Net;

namespace AbySalto.Mid.Domain.DTOs.Responses
{
    public class ErrorResponseDto
    {
        public string Message { get; set; }
        public long? MessageCode { get; set; }
        public HttpStatusCode Code { get; set; }
        public string Stacktrace { get; set; }
        public ICollection<ValidationFieldErrorDto> Errors { get; set; }
    }

    public class ValidationFieldErrorDto
    {
        public string Field { get; set; }
        public string Error { get; set; }
        public object Extra { get; set; }
    }
}
