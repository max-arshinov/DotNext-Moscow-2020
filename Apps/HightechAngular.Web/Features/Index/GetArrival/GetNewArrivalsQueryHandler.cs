using Force.Cqrs;
using HightechAngular.Orders.Entities;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace HightechAngular.Web.Features.Index.GetArrival
{
    public class GetNewArrivalsQueryHandler : IQueryHandler<GetNewArrivalsQuery, IEnumerable<GetNewArrivalsListItem>>
    {
        private readonly IQueryable<Product> _products;
        public GetNewArrivalsQueryHandler(IQueryable<Product> products)
        {
            _products = products;
        }
        public IEnumerable<GetNewArrivalsListItem> Handle(GetNewArrivalsQuery input) =>
          _products
             .ProjectToType<GetNewArrivalsListItem>()
             .OrderByDescending(x => x.DateCreated)
             .Take(4);
    }
}
