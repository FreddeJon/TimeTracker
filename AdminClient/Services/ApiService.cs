using System.Net.Http.Headers;

namespace AdminClient.Services;

public interface IApiService
{
    Task<HttpClient> GetClient(HttpContext context);
}

public class ApiService : IApiService
{
    private const string BasePath = "https://localhost:5003/api/";


    public async Task<HttpClient> GetClient(HttpContext context)
    {
        var accessToken = await context.GetTokenAsync("access_token");
        var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        client.BaseAddress = new Uri(BasePath);
        return client;
    }
}
