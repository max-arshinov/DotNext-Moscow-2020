using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Workflow
{
    public class FailureInfo
    {
        public FailureInfo(FailureType type, string message = null)
        {
            Type = type;
            Message = message;
        }

        public FailureType Type { get; }

        public string Message { get; }

        public static FailureInfo Exception(Exception e, string message = null)
        {
            return new ExceptionFailureInfo(e, message);
        }

        public static FailureInfo Invalid(string message)
        {
            return new FailureInfo(FailureType.Invalid, message);
        }

        public static FailureInfo Invalid(IEnumerable<ValidationResult> errors)
        {
            return new ValidationFailureInfo(FailureType.Invalid, errors);
        }

        public static FailureInfo Unauthorized(string message)
        {
            return new FailureInfo(FailureType.Unauthorized, message);
        }

        public static FailureInfo ConfigurationError(string message)
        {
            return new FailureInfo(FailureType.ConfigurationError, message);
        }

        public static FailureInfo Other(string message)
        {
            return new FailureInfo(FailureType.Other, message);
        }
    }
}