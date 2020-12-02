using System;
using System.Reflection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Tests
{
    public static class Services
    {
        public static IServiceProvider BuildServiceProvider<T>(
            Func<IServiceProvider, T> dbContextFactory,
            params Assembly[] assemblies)
            where T : DbContext
        {
            var sc = new ServiceCollection();
            sc.AddDbContextAndQueryables(dbContextFactory);
            foreach (var assembly in assemblies)
            {
                sc.AddModule(assembly);
                sc.AddMediatR(assembly);
            }

            return sc.BuildServiceProvider();
        }
    }
}