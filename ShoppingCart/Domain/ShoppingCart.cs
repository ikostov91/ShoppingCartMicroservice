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
    
        public void AddItems(IEnumerable<ShoppingCartItem> items)
        {
            foreach (var item in items)
            {
                this._items.Add(item);
            }
        }

        public void RemoveItems(int[] productCatalogueIds)
        {
            this._items.RemoveWhere(x => productCatalogueIds.Contains(x.ProductCatalogueId));
        }
    }

    public record ShoppingCartItem(int ProductCatalogueId, string PtoductName, string Description, Money Price)
    {
        public virtual bool Equals(ShoppingCartItem? obj)
        {
            return obj != null && this.ProductCatalogueId.Equals(obj.ProductCatalogueId);
        }

        public override int GetHashCode()
        {
            return this.ProductCatalogueId.GetHashCode();
        }
    }

    public record Money(string Currency, decimal Amount);
}
