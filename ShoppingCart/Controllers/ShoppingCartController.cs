using Microsoft.AspNetCore.Mvc;
using ShoppingCartNamespace.Contracts;
using ShoppingCartNamespace.Domain;

namespace ShoppingCartNamespace.Controllers
{
    [Route("/shoppingcart")]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartStore _shoppingCartStore;
        private readonly IProductCatalogueClient _productCatalogueClient;

        public ShoppingCartController(
            IShoppingCartStore shoppingCartStore,
            IProductCatalogueClient productCatalogueClient)
        {
            this._shoppingCartStore = shoppingCartStore;
            this._productCatalogueClient = productCatalogueClient;
        }

        [HttpGet("{userId:int}")]
        public ShoppingCart Get(int userId)
        {
            return this._shoppingCartStore.Get(userId);
        }

        [HttpPost("{userId:int}")]
        public async Task<ShoppingCart> Post(int userId, [FromBody] int[] productIds)
        {
            var shoppingCart = this._shoppingCartStore.Get(userId);
            var shoppingCartItems = await this._productCatalogueClient.GetShoppingCartItems(productIds);
            shoppingCart.AddItems(shoppingCartItems);
            this._shoppingCartStore.Save(shoppingCart);

            return shoppingCart;
        }
    }
}
