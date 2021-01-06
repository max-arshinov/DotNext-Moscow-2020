using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Force.Ccc;
using Force.Reflection;

namespace Infrastructure.Validation
{
    public class DataAnnotationValidator<T> : IValidator<T>, IAsyncValidator<T>
    {
        public virtual Task<IEnumerable<ValidationResult>> ValidateAsync(T obj)
        {
            return Task.FromResult(Validate(obj));
        }

        public virtual IEnumerable<ValidationResult> Validate(T obj)
        {
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(obj, new ValidationContext(obj), results, true);
            return results;
        }
    }
}