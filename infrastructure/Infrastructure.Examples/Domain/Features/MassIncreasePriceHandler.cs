using System.Linq;
using Force.Cqrs;
using Infrastructure.Examples.Domain.Entities;
using JetBrains.Annotations;

namespace Infrastructure.Examples.Domain.Features
{
    [UsedImplicitly]
    public class MassIncreasePriceHandler: ICommandHandler<MassIncreasePrice, int>
    {
        private readonly IQueryable<Product> _products;

        public MassIncreasePriceHandler(IQueryable<Product> products)
        {
            _products = products;
        }

        public int Handle(MassIncreasePrice command) =>
            _products
                .Where(Product.Specs.IsForSale)
                .MassIncreasePrice(command);
    }
}