using Force.Cqrs;
using HightechAngular.Orders.Entities;
using HightechAngular.Web.Dto;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HightechAngular.Web.Features.Index.NewArrivals
{
    public class GetNewArrivalsQueryHandler :
        IQueryHandler<GetNewArrivals, IEnumerable<NewArrivalsListItem>>
    {
        private readonly IQueryable<Product> _products;
        public GetNewArrivalsQueryHandler(IQueryable<Product> products)
        {
            _products = products;
        }
        public IEnumerable<NewArrivalsListItem> Handle(GetNewArrivals input) =>
            _products
                .ProjectToType<NewArrivalsListItem>()
                .OrderByDescending(x => x.DateCreated)
                .Take(4)
                .ToList();
    }
}
