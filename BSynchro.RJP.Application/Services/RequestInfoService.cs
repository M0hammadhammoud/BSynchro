using BSynchro.RJP.Accounts.Application.Contracts;
using Microsoft.AspNetCore.Http;

namespace BSynchro.RJP.Accounts.Application.Services
{
    public class RequestInfoService : IRequestInfoService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public string? CorrelationId { get; set; }

        //RequestInfoService manages all request info like correlation id passing when communication between microservices
        public RequestInfoService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            var currentContext = _httpContextAccessor.HttpContext;
            if (currentContext is null)
                return;
            GenerateOrSetCorrelationId(currentContext);
        }

        private void GenerateOrSetCorrelationId(HttpContext httpContext)
        {
            if (httpContext.Request.Headers.TryGetValue("X-Correlation-Id", out var correlationId))
            {
                CorrelationId = correlationId.ToString();
            }

            if (string.IsNullOrEmpty(CorrelationId))
            {
                CorrelationId = Guid.NewGuid().ToString(); ;
                httpContext.Response.Headers.Add("X-Correlation-Id", CorrelationId);
            }
        }
    }
}
