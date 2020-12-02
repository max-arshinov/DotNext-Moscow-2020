using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Infrastructure.Validation;
using JetBrains.Annotations;

namespace Infrastructure.Examples.Domain.Features
{
    [UsedImplicitly]
    public class UpdateProductValidator: IAsyncValidator<UpdateProductAsyncContext>
    {
        public Task<IEnumerable<ValidationResult>> ValidateAsync(UpdateProductAsyncContext obj)
        {
            return Task.FromResult<IEnumerable<ValidationResult>>(new[]
            {
                new ValidationResult("Something is wrong")
            });
        }
    }
}