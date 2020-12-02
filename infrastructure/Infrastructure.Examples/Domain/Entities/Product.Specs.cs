using Force.Ddd;

namespace Infrastructure.Examples.Domain.Entities
{
    public class ProductSpecs
    {
        internal ProductSpecs()
        {
        }

        public Spec<Product> IsForSale { get; } = new Spec<Product>(x => x.Price > 0);
    }
}