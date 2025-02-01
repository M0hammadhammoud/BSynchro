namespace Common.Utitlities.Contracts
{
    public interface IHttpClientHelper
    {
        void SetDefaultHeaders(Dictionary<string, string> headers);
        Task<T> GetAsync<T>(string resource);
        Task<T1> PostAsync<T, T1>(string resource, T requestBody);
        Task<T1> PutAsync<T, T1>(string resource, T requestBody);
        Task DeleteAsync(string resource);
    }
}
