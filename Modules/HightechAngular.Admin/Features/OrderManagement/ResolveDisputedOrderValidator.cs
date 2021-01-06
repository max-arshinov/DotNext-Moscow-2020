using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Infrastructure.Validation;

namespace HightechAngular.Admin.Features.OrderManagement
{
    public class ResolveDisputedOrderValidator//: IAsyncValidator<ResolveDisputedOrder>
    {
        public Task<IEnumerable<ValidationResult>> ValidateAsync(ResolveDisputedOrder obj)
        {
            return Task.FromResult((IEnumerable<ValidationResult>)
                new[] {ValidationResult.Success});
        }
    }
}