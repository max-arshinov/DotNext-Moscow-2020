using Force.Cqrs;
using System.Collections.Generic;

namespace HightechAngular.Web.Features.Index.GetBestSellers
{
    public class GetBestsellersQuery : 
        FilterQuery<GetBestsellersListItem>,
        IQuery<IEnumerable<GetBestsellersListItem>>
    {
    }
}