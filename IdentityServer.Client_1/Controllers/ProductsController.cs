using IdentityServer.Client_1.Models;
using IdentityServer.Client_1.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IdentityServer.Client_1.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IApiResourceHttpClient _apiResourceHttpClient;

        public ProductsController(IConfiguration configuration, IApiResourceHttpClient apiResourceHttpClient)
        {
            _configuration = configuration;
            _apiResourceHttpClient = apiResourceHttpClient;
        }

        public async Task<IActionResult> Index()
        {
            HttpClient httpClient = await _apiResourceHttpClient.GetHttpClient();
            List<Product> products = new();

            var response = await httpClient.GetAsync("https://localhost:7289/api/products/getproducts");

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
