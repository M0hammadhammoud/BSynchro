using System.Net;
using System.Text.Json;

namespace BSynchro.RJP.Transactions.WebAPI.Models.Responses
{
    public class BaseResponse
    {
        public HttpStatusCode HttpStatusCode { get; set; } = HttpStatusCode.OK;
        public string? Message { get; set; }
        public List<string> ValidationErrors { get; set; } = [];
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
