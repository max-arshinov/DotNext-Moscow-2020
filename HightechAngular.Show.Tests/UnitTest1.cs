using System.Collections.Generic;
using HightechAngular.Shop.Features.Catalog;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace HightechAngular.Show.Tests
{
    public class UnitTest1: IClassFixture<TestContextFixture>
    {
        private readonly TestContextFixture _testContextFixture;

        public UnitTest1(TestContextFixture testContextFixture)
        {
            _testContextFixture = testContextFixture;
        }

        [Fact]
        public void Test1()
        {
            var result = _testContextFixture
                .With<CatalogController>()
                .Execute(c => c.Get(new GetProducts()
                {
                    CategoryId = 2
                }));

            Assert.NotEmpty(result);
        }
    }
}