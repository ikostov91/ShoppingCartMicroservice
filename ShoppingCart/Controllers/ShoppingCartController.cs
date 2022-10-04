using Microsoft.AspNetCore.Mvc;
using ShoppingCartNamespace.Contracts;
using ShoppingCartNamespace.Domain;
using System.Text.Json;

namespace ShoppingCartNamespace.Controllers
{
    [Route("/shoppingcart")]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartStore _shoppingCartStore;
        private readonly IProductCatalogueClient _productCatalogueClient;
        private readonly IEventStore _eventStore;

        public ShoppingCartController(
            IShoppingCartStore shoppingCartStore,
            IProductCatalogueClient productCatalogueClient,
            IEventStore eventStore)
        {
            this._shoppingCartStore = shoppingCartStore;
            this._productCatalogueClient = productCatalogueClient;
            this._eventStore = eventStore;
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
            shoppingCart.AddItems(shoppingCartItems, this._eventStore);
            this._shoppingCartStore.Save(shoppingCart);

            return shoppingCart;
        }

        [HttpDelete("{userId:int}")]
        public ShoppingCart Delete(int userId, [FromBody] int[] productIds)
        {
            var shoppingCard = this._shoppingCartStore.Get(userId);
            shoppingCard.RemoveItems(productIds, this._eventStore);
            this._shoppingCartStore.Save(shoppingCard);

            return shoppingCard;
        }
    }
}
