using ShoppingCartNamespace.Contracts;

namespace ShoppingCartNamespace.Domain
{
    public class ShoppingCart
    {
        private readonly HashSet<ShoppingCartItem> _items = new();

        public ShoppingCart(int userId)
        {
            this.UserId = userId;
        }

        public int UserId { get; set; }
        public IEnumerable<ShoppingCartItem> Items => this._items;
    
        public void AddItems(IEnumerable<ShoppingCartItem> items, IEventStore eventStore)
        {
            foreach (var item in items)
            {
                if (this._items.Add(item))
                {
                    eventStore.Raise("ShoppingCartItemAdded", new { UserId, item });
                }
            }
        }

        public void RemoveItems(int[] productCatalogIds, IEventStore eventStore)
        {
            this._items.RemoveWhere(x => productCatalogIds.Contains(x.ProductCatalogId));
            eventStore.Raise("ShoppingCartItemsRemoved", new { ProductCatalogIds = productCatalogIds });
        }
    }

    public record ShoppingCartItem(int ProductCatalogId, string PtoductName, string Description, Money Price)
    {
        public virtual bool Equals(ShoppingCartItem? obj)
        {
            return obj != null && this.ProductCatalogId.Equals(obj.ProductCatalogId);
        }

        public override int GetHashCode()
        {
            return this.ProductCatalogId.GetHashCode();
        }
    }

    public record Money(string Currency, decimal Amount);
}
