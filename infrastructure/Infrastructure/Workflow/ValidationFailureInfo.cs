using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Workflow
{
    public class ValidationFailureInfo : FailureInfo
    {
        public ValidationFailureInfo(
            FailureType type,
            IEnumerable<ValidationResult> results) :
            base(type, GetMessage(results))
        {
            Results = results;
        }

        public IEnumerable<ValidationResult> Results { get; }

        private static string GetMessage(IEnumerable<ValidationResult> results)
        {
            return "One or more validation errors occurred.";
        }
    }
}