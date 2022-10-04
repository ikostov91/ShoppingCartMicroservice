using ShoppingCartNamespace.Domain;

namespace ShoppingCartNamespace.Contracts
{
    public interface IShoppingCartStore
    {
        ShoppingCart Get(int userId);
        void Save(ShoppingCart shoppingCart);
    }
}
