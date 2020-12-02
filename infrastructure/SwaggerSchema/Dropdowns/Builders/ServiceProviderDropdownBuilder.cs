using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.SwaggerSchema.Dropdowns.Builders
{
    internal class ServiceProviderDropdownBuilder<T>:
        IDropdownBuilder
        where T : class
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Func<IQueryable<T>, IServiceProvider, IQueryable<DropdownOption>> _map;
        private readonly string _name;

        internal ServiceProviderDropdownBuilder(
            IServiceProvider serviceProvider, 
            Func<IQueryable<T>, IServiceProvider, IQueryable<DropdownOption>> map, 
            string name)
        {
            _serviceProvider = serviceProvider;
            _map = map;
            _name = name;
        }

        public async Task<Dropdown> BuildAsync()
        {
            using var scope = _serviceProvider.CreateScope();
            var dbc = (DbContext) scope.ServiceProvider.GetService(typeof(DbContext));
            return new Dropdown(await _map(dbc.Set<T>(), _serviceProvider).ToListAsync(), _name);
        }
    }
}