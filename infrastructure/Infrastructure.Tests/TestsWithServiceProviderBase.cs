using System;
using Infrastructure.Examples.Data;
using Infrastructure.Examples.Domain.Features;

namespace Infrastructure.Tests
{
    public abstract class TestsWithServiceProviderBase
    {
        private readonly IServiceProvider _serviceProvider;

        protected IServiceProvider GetServiceProvider()
        {
            return _serviceProvider;
        }

        protected TestsWithServiceProviderBase()
        {
            _serviceProvider = Services.BuildServiceProvider(
                sp => new ExampleDbContext(),
                typeof(ProductController).Assembly);
        }
    }
}