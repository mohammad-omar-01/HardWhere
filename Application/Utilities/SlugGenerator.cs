using Domain.ProductNS;

namespace Application.Utilities
{
    public class SlugGenerator : IStringRandomGenarotor<SlugGenerator>
    {
        public string GenrateRandomString(Product product)
        {
            return product.Name
                + "-"
                + product.DateAdded.GetValueOrDefault().Month.ToString()
                + product.ProductId;
        }
    }
}
