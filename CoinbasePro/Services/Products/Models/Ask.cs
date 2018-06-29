namespace CoinbasePro.Services.Products.Models
{
    public class Ask : Quote
    {
        public Ask(
            decimal price,
            decimal size)
                : base(price, size)
        {
        }
    }
}
