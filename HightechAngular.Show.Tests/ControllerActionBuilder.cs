using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HightechAngular.Show.Tests
{
    public class ControllerActionBuilder<T>
        where T: ControllerBase, new()
    {
        private readonly TestContextFixture _testContextFixture;
        
        public ControllerActionBuilder(TestContextFixture testContextFixture)
        {
            _testContextFixture = testContextFixture;
        }

        public TResult Execute<TResult>(Func<T, ActionResult<TResult>> action)
        {
            var controller = new T
            {
                ControllerContext = new ControllerContext()
                {
                    HttpContext = new DefaultHttpContext()
                    {
                        RequestServices = _testContextFixture.Host.Services
                    }
                }
            };

            var result = action(controller);
            TResult r = ((dynamic) result).Result.Value;
            return r;
        }
    }
}