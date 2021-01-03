using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Force.Ccc;
using Infrastructure.Cqrs;
using Infrastructure.Validation;
using Infrastructure.Workflow;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Infrastructure.AspNetCore
{
    public class ActionResultBuilder<T> : IStatusCodeActionResult
    {
        private readonly HttpContext _httpContext;
        private readonly Result<T, FailureInfo> _result;

        private ActionResult _objectResult;

        public ActionResultBuilder(Result<T, FailureInfo> result, HttpContext httpContext)
        {
            _result = result;
            _httpContext = httpContext;
        }

        Task IActionResult.ExecuteResultAsync(ActionContext context)
        {
            var res = GetActionResult(_result);
            return res.ExecuteResultAsync(context);
        }

        int? IStatusCodeActionResult.StatusCode => (GetActionResult(_result) as IStatusCodeActionResult)?.StatusCode;


        public static implicit operator ActionResult<T>(ActionResultBuilder<T> resultBuilder)
        {
            return resultBuilder.GetActionResult(resultBuilder._result);
        }

        private ActionResult GetActionResult(Result<T, FailureInfo> result)
        {
            return _objectResult ??= Match(result);
        }

        private static Dictionary<string, IEnumerable<string>> GetErrors(FailureInfo failureInfo)
        {
            return failureInfo switch
            {
                ValidationFailureInfo vfi => GetValidationFailureInfo(vfi),
                _ => EmptyResults.EmptyErrors
            };
        }

        private static Dictionary<string, IEnumerable<string>> GetValidationFailureInfo(
            ValidationFailureInfo failureInfo)
        {
            var dictionary = new Dictionary<string, IEnumerable<string>>();
            foreach (var validationResult in failureInfo.Results)
            {
                if (validationResult.MemberNames.Any())
                {
                    foreach (var memberName in validationResult.MemberNames)
                    {
                        AddToDictionaryAndCreateIfNeeded(dictionary, memberName, validationResult);
                    }
                }
                else
                {
                    AddToDictionaryAndCreateIfNeeded(dictionary, "", validationResult);
                }
            }

            return dictionary;
        }

        private static void AddToDictionaryAndCreateIfNeeded(IDictionary<string, IEnumerable<string>> dictionary,
            string key, ValidationResult fi)
        {
            if (!dictionary.ContainsKey(key))
            {
                dictionary[key] = new List<string>();
            }

            ((List<string>) dictionary[key]).Add(fi.ErrorMessage);
        }

        protected virtual ActionResult Match(Result<T, FailureInfo> result)
        {
            return result.Match(SuccessMatch, FailureMatch);
        }


        public static string GetErrorDetails(FailureInfo failureInfo)
        {
            return string.Join(",\n",
                GetErrors(failureInfo)
                    .Select(kv => kv.Key + ": " + kv.Value)
                    .ToArray());
        }

        #region Public API for overriding the response handling

        public IActionResult Match(Func<Result<T, FailureInfo>, HttpContext, IActionResult> matchFunc)
        {
            return matchFunc(_result, _httpContext);
        }

        public IActionResult Match(Func<Result<T, FailureInfo>, IActionResult> matchFunc)
        {
            return matchFunc(_result);
        }

        public IActionResult Match(Func<T, HttpContext, IActionResult> successFunc,
            Func<FailureInfo, HttpContext, IActionResult> failureFunc)
        {
            return _result.Match(success => successFunc(success, _httpContext),
                failureInfo => failureFunc(failureInfo, _httpContext));
        }

        public IActionResult Match(Func<T, IActionResult> successFunc, Func<FailureInfo, IActionResult> failureFunc)
        {
            return _result.Match(successFunc, failureFunc);
        }

        public IActionResult Match(Func<T, IActionResult> successFunc)
        {
            return _result.Match(successFunc, FailureMatch);
        }

        public IActionResult Match(Func<T, HttpContext, IActionResult> successFunc)
        {
            return _result.Match(success => successFunc(success, _httpContext), FailureMatch);
        }


        public IActionResult Match(Func<FailureInfo, IActionResult> failureFunc)
        {
            return _result.Match(SuccessMatch, failureFunc);
        }

        public IActionResult Match(Func<FailureInfo, HttpContext, IActionResult> failureFunc)
        {
            return _result.Match(SuccessMatch, failureInfo => failureFunc(failureInfo, _httpContext));
        }

        #endregion

        #region Default response handling (to use in Match)

        public static ObjectResult SuccessMatch(T success)
        {
            return success switch
            {
                null when typeof(T) == typeof(object) => new ObjectResult(null)
                {
                    StatusCode = StatusCodes.Status204NoContent
                },
                null => new ObjectResult(null) {StatusCode = StatusCodes.Status404NotFound},
                _ => MatchNotNull(success)
            };
        }

        private static ObjectResult DispatchResult<TResult>(CommandResult<TResult> commandResult) =>
            commandResult.Match(s => new OkObjectResult(s), FailureMatch);
        
        private static ObjectResult DispatchResult(object result) => new OkObjectResult(result);

        private static ObjectResult MatchNotNull(T success) => DispatchResult((dynamic) success);

        public static ObjectResult FailureMatch(FailureInfo failureInfo)
        {
            var statusCode = failureInfo.Type switch
            {
                FailureType.Invalid => (failureInfo as ValidationFailureInfo)
                                       ?.Results.Any(y => y is NotFoundValidationResult)
                                       == true
                    ? StatusCodes.Status404NotFound
                    : StatusCodes.Status422UnprocessableEntity,
                FailureType.Unauthorized => StatusCodes.Status401Unauthorized,
                FailureType.ConfigurationError => StatusCodes.Status501NotImplemented,
                _ => StatusCodes.Status500InternalServerError
            };

            if (statusCode >= 500 && statusCode <= 599)
            {
                return new ObjectResult(new ProblemDetails
                {
                    Detail = GetErrorDetails(failureInfo),
                    Title = failureInfo.Message,
                    Status = statusCode
                })
                {
                    StatusCode = statusCode
                };
            }

            var descriptor = GetErrors(failureInfo)
                .ToDictionary(pair => pair.Key,
                    pair => pair.Value.ToArray());

            return new BadRequestObjectResult(new ValidationProblemDetails(descriptor))
            {
                StatusCode = statusCode
            };
        }

        #endregion
    }
}