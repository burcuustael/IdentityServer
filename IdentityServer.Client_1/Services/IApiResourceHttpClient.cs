namespace IdentityServer.Client_1.Services
{
    public interface IApiResourceHttpClient
    {
        Task<HttpClient> GetHttpClient();
    }
}
