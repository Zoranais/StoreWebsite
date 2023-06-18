namespace TestStore.Models
{
    public class Cart
    {
        private List<CartLine> CartLines = new List<CartLine>();
        public virtual void AddItem(Product product, int quantity)
        {
            CartLine line = CartLines.
                Where(x=>x.Product.ProductId==product.ProductId).FirstOrDefault();
            if (line == null)
            {
                CartLines.Add(new CartLine { Product = product, Quantity = quantity });

            }
            else
            {
                line.Quantity += quantity;
            }
        }
        public virtual void RemoveItem(Product product) =>
            CartLines.RemoveAll(l => l.Product.ProductId == product.ProductId);
        public virtual IEnumerable<CartLine> GatCartLines => CartLines;
        public virtual void ClearCart()=>CartLines.Clear();
        public virtual decimal GetTotalPrice()=>CartLines.Sum(x=>x.Product.Price*x.Quantity);

    }
    public class CartLine
    {
        public int CartLineId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
