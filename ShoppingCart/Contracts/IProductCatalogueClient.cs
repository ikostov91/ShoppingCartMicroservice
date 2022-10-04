using ShoppingCartNamespace.Domain;

namespace ShoppingCartNamespace.Contracts
{
    public interface IProductCatalogueClient
    {
        Task<IEnumerable<ShoppingCartItem>> GetShoppingCartItems(int[] productCatalogueIds);
    }
}
