using Infrastructure.Cqrs;
using Infrastructure.Examples.Domain.Entities;
using JetBrains.Annotations;

namespace Infrastructure.Examples.Domain.Features
{
    [UsedImplicitly] // just to tell Rider that this handler IS used and it's just not smart enough to figure out how
    public class CreateExampleHandler: CreateIntEntityHandlerBase<CreateProduct, Product>
    {
        protected override Product CreateNewEntity(CreateProduct input) =>
            new Product(input);
    }
}