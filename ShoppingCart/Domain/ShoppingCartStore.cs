using ShoppingCartNamespace.Contracts;

namespace ShoppingCartNamespace.Domain
{
    public class ShoppingCartStore : IShoppingCartStore
    {
        private readonly Dictionary<int, ShoppingCart> _database = new Dictionary<int, ShoppingCart>();

        public ShoppingCart Get(int userId)
        {
            return this._database.ContainsKey(userId) ? this._database[userId] : new ShoppingCart(userId);
        }

        public void Save(ShoppingCart shoppingCart)
        {
            this._database[shoppingCart.UserId] = shoppingCart;
        }
    }
}
