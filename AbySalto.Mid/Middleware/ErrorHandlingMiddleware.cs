using Newtonsoft.Json;
using AbySalto.Mid.Domain.Core.Exceptions;
using System.Net;
using AbySalto.Mid.Domain.Enums;
using AbySalto.Mid.Domain.DTOs.Responses;
using Newtonsoft.Json.Serialization;

namespace AbySalto.Mid.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = HttpStatusCode.InternalServerError;
            var stacktrace = "🥞";

#if DEBUG
            stacktrace = ex.StackTrace;
#endif

            if (ex is NotFoundException)
            {
                code = HttpStatusCode.NotFound;
            }

            if (ex is ForbiddenException)
            {
                code = HttpStatusCode.Forbidden;
            }

            if (ex is BadRequestException)
            {
                code = HttpStatusCode.BadRequest;
            }

            var responseMessage = new ErrorResponseDto
            {
                Message = ex.Message,
                Code = code,
                Stacktrace = stacktrace
            };

            Enum.TryParse<ErrorCode>(responseMessage.Message, out var messageCode);

            if (Enum.IsDefined(typeof(ErrorCode), messageCode))
            {
                responseMessage.MessageCode = (long)messageCode;
            }

            var result = JsonConvert.SerializeObject(responseMessage, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
