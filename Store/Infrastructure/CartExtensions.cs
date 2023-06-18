using TestStore.Models;

namespace TestStore.Infrastructure
{
    public static class CartExtensions
    {
        public static decimal GetLinePrice(this CartLine cartLine)
        {
            return cartLine.Product.Price * cartLine.Quantity;
        }

    }
}
