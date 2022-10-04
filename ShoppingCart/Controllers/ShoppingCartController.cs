using Microsoft.AspNetCore.Mvc;
using ShoppingCartNamespace.Contracts;
using ShoppingCartNamespace.Domain;

namespace ShoppingCartNamespace.Controllers
{
    [Route("/shoppingcart")]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartStore _shoppingCartStore;

        public ShoppingCartController(IShoppingCartStore shoppingCartStore)
        {
            this._shoppingCartStore = shoppingCartStore;
        }

        [HttpGet("{userId:int}")]
        public ShoppingCart Get(int userId)
        {
            return this._shoppingCartStore.Get(userId);
        }
    }
}
