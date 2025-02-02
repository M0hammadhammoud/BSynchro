using BSynchro.RJP.Transactions.Application.Constants;
using BSynchro.RJP.Transactions.Application.Contracts;
using BSynchro.RJP.Transactions.WebAPI.Models.Responses;
using Serilog.Context;
using System.Net;
using FluentValidation;
using System.Text.Json;

namespace BSynchro.RJP.Transactions.WebAPI.Middlewares
{
    //this middleware is a global exception handler that handles and logs exception using serilog
    public class InterceptorMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<InterceptorMiddleware> _logger;

        public InterceptorMiddleware(RequestDelegate next,
                                     ILogger<InterceptorMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext, IRequestInfoService requestInfoService)
        {
            try
            {
                SetLoggerProperties(httpContext, requestInfoService);
                await _next(httpContext);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Exception occured in {httpContext.Request.Path}");
                await HandleExceptionAsync(httpContext, exception);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.ContentType = "application/json";

            var response = new BaseResponse();

            switch (exception)
            {
                case ValidationException validationException:
                    // Handle FluentValidation errors (400 Bad Request)
                    httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.HttpStatusCode = HttpStatusCode.BadRequest;
                    response.Message = BusinessMessages.ValidationFailed;
                    response.ValidationErrors = validationException.Errors.Select(e => e.ErrorMessage).ToList();
                    break;

                case KeyNotFoundException:
                    // Handle not found errors (404)
                    httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    response.HttpStatusCode = HttpStatusCode.NotFound;
                    response.Message = exception.Message;
                    break;

                default:
                    // Handle other unhandled exceptions (500)
                    httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response.HttpStatusCode = HttpStatusCode.InternalServerError;
                    response.Message = BusinessMessages.ErrorOccurred;
                    break;
            }

            // Serialize response and return
            var jsonResponse = JsonSerializer.Serialize(response);
            await httpContext.Response.WriteAsync(jsonResponse);
        }

        private void SetLoggerProperties(HttpContext httpContext, IRequestInfoService requestInfoService)
        {
            LogContext.PushProperty("Path", $"{httpContext.Request.Path}");

            if (!string.IsNullOrEmpty(requestInfoService.CorrelationId))
            {
                LogContext.PushProperty("CorrelationId", $"{requestInfoService.CorrelationId}");
            }
        }
    }
}