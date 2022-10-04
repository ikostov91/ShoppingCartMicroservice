using ShoppingCartNamespace.Contracts;
using ShoppingCartNamespace.Domain;
using System.Net.Http.Headers;

namespace ShoppingCartNamespace
{
    public class ProductCatalogueClient : IProductCatalogueClient
    {
        private readonly HttpClient _client;
        private static string productCatalogBaseUrl = @"https://git.io/JeHiE";
        private static string getProductPathTemplate = "?productIds=[{0}]";

        public ProductCatalogueClient(HttpClient client)
        {
            client.BaseAddress = new Uri(productCatalogBaseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            this._client = client;
        }

        public Task<IEnumerable<ShoppingCartItem>> GetShoppingCartItems(int[] productCatalogueIds)
        {
            throw new NotImplementedException();
        }

        private async Task<HttpResponseMessage> RequestProductFromProductCatalogue(int[] productCatalogueIds)
        {
            var productsResource = string.Format(getProductPathTemplate, string.Join(",", productCatalogBaseUrl));

            return await this._client.GetAsync(productsResource);
        }
    }
}
