using Domain.ProductNS;

namespace Application.Utilities
{
    public class SKUGenerator : IStringRandomGenarotor<SKUGenerator>
    {
        public string GenrateRandomString(Product product)
        {
            return product.ProductId + GenerateRandomNumber(product.ProductId).ToString() + 119;
        }

        private int GenerateRandomNumber(int limit)
        {
            Random rnd = new Random(limit);
            return rnd.Next(10, 300);
        }
    }
}
