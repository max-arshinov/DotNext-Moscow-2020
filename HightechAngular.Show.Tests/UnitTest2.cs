using System.Collections.Generic;
using HightechAngular.Shop.Features.Catalog;
using Infrastructure.Workflow;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Xunit.Sdk;

namespace HightechAngular.Show.Tests
{
    public class UnitTest2: IClassFixture<TestContextFixture>
    {
        private readonly TestContextFixture _testContextFixture;

        public UnitTest2(TestContextFixture testContextFixture)
        {
            _testContextFixture = testContextFixture;
        }

        [Fact]
        public void Test2()
        {
            var w = _testContextFixture.Host.Services.GetService<IWorkflow<GetProducts, IEnumerable<ProductListItem>>>();
            var result = w.Process(new GetProducts()
            {
                CategoryId = 2
            }, _testContextFixture.Host.Services);

            var v = result.Match(s => s, f => throw new AssertActualExpectedException(1, 2, "3"));
            Assert.NotEmpty(v);
        }
    }
}