using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace IdentityServer.Client_1.Services
{
    public class ApiResourceHttpClient : IApiResourceHttpClient
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private HttpClient _httpClient;
        public ApiResourceHttpClient(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
            _httpClient = new HttpClient();
        }

        public async Task<HttpClient> GetHttpClient()
        {
            var accessToken = await _contextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

            _httpClient.SetBearerToken(accessToken);
            return _httpClient;
        }
    }
}
