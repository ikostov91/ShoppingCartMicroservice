using ShoppingCartNamespace.Contracts;
using ShoppingCartNamespace.Domain;
using System.Net.Http.Headers;
using System.Text.Json;

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

        public async Task<IEnumerable<ShoppingCartItem>> GetShoppingCartItems(int[] productCatalogueIds)
        {
            using var response = await this.RequestProductFromProductCatalogue(productCatalogueIds);

            return await ConvertToShoppingCartItems(response);
        }

        private async Task<HttpResponseMessage> RequestProductFromProductCatalogue(int[] productCatalogueIds)
        {
            var productsResource = string.Format(getProductPathTemplate, string.Join(",", productCatalogBaseUrl));

            return await this._client.GetAsync(productsResource);
        }

        private static async Task<IEnumerable<ShoppingCartItem>> ConvertToShoppingCartItems(HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();

            var products = await JsonSerializer.DeserializeAsync<List<ProductCatalogProduct>>(await response.Content.ReadAsStreamAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                }) ?? new();

            return products.Select(p => new ShoppingCartItem(p.ProductId, p.ProductName, p.ProductDescription, p.Price));
        }

        private record ProductCatalogProduct(int ProductId, string ProductName, string ProductDescription, Money Price);
    }
}
