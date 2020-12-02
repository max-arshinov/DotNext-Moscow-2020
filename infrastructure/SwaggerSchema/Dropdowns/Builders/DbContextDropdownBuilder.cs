using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.SwaggerSchema.Dropdowns.Builders
{
    internal class DbContextDropdownBuilder<T>:
        DropdownBuilder
        where T : class
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Func<IQueryable<T>, IQueryable<DropdownOption>> _map;
        private readonly string _name;

        internal DbContextDropdownBuilder(
            IServiceProvider serviceProvider, 
            Func<IQueryable<T>, IQueryable<DropdownOption>> map, 
            string name)
        {
            _serviceProvider = serviceProvider;
            _map = map;
            _name = name;
        }

        public override async Task<Dropdown> BuildAsync()
        {
            using var scope = _serviceProvider.CreateScope();
            var queryable = scope.ServiceProvider.GetService<IQueryable<T>>();
            return new Dropdown(await _map(queryable).ToListAsync(), _name);
        }
    }
}