using System.Threading.Tasks;
using Force.Ccc;
using Force.Cqrs;
using Infrastructure.Examples.Domain.Entities;
using JetBrains.Annotations;

namespace Infrastructure.Examples.Domain.Features
{
    [UsedImplicitly]
    public class CreateExampleAsyncHandler: ICommandHandler<CreateProductAsync, Task<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateExampleAsyncHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<int> Handle(CreateProductAsync input)
        {
            var product = new Product(new CreateProduct
            {
                Name = input.Name,
                Price = input.Price
            });
            _unitOfWork.Add(product);
            
            #warning CommitAsync
            _unitOfWork.Commit();
            return Task.FromResult(product.Id);
        }
    }
}