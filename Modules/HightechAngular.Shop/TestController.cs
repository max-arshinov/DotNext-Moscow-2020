using Force.Ccc;
using Force.Cqrs;
using HightechAngular.Orders.Entities;
using Infrastructure.AspNetCore;
using Microsoft.AspNetCore.Mvc;

namespace HightechAngular.Shop
{
    public class TestController: ApiControllerBase
    {
        public class B: ICommand
        {
            
        }
        
        public class BHandler: ICommandHandler<B>
        {
            private readonly IUnitOfWork _unitOfWork;

            public BHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }
            
            public void Handle(B input)
            {
                _unitOfWork.Add(new Category("zzzz"));
            }
        }
        
        [HttpGet]        
        public IActionResult A()
        {
            this.Process(new B());
            return Ok();
        }
    }
}