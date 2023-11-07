using IdentityModel.Client;
using IdentityServer.Client_1.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IdentityServer.Client_1.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IConfiguration _configuration;

        public ProductsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            List<Product> products = new();
            HttpClient client = new HttpClient();

            var disco = await client.GetDiscoveryDocumentAsync("https://localhost:7148");

            if (disco.IsError)
            {
                //loglama yap
            }

            ClientCredentialsTokenRequest clientCredentialsTokenRequest = new ClientCredentialsTokenRequest();
            clientCredentialsTokenRequest.ClientId = _configuration["Client:ClientId"];
            clientCredentialsTokenRequest.ClientSecret = _configuration["Client:ClientSecret"];
            clientCredentialsTokenRequest.Address = disco.TokenEndpoint;

            var token = await client.RequestClientCredentialsTokenAsync(clientCredentialsTokenRequest);

            if (token.IsError)
            {
                //loglama yap
            }

            client.SetBearerToken(token.AccessToken);

            var response = await client.GetAsync("https://localhost:7289/api/products/getproducts");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                products = JsonConvert.DeserializeObject<List<Product>>(content);
            }

            else
            { //loglama yap
            }   
            return View(products);
        }
    }
}
