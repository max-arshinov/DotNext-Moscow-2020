using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;

namespace Infrastructure.Validation
{
    public class NotFoundValidationResult: ValidationResult
    {
        protected NotFoundValidationResult([NotNull] ValidationResult validationResult) : base(validationResult)
        {
        }

        public NotFoundValidationResult(string errorMessage) : base(errorMessage)
        {
        }

        public NotFoundValidationResult(string errorMessage, IEnumerable<string> memberNames) : 
            base(errorMessage, memberNames)
        {
        }
    }
}