using System.Linq;
using System.Threading.Tasks;
using Extensions.Hosting.AsyncInitialization;
using HightechAngular.Identity.Entities;
using HightechAngular.Orders.Entities;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HightechAngular.Data
{
    [UsedImplicitly]
    public class ApplicationDbContextInitializer : IAsyncInitializer
    {
        private readonly ApplicationDbContext _context;

        public ApplicationDbContextInitializer(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
        }

        private async Task SeedEverything()
        {
            var categories = new []
            {
                new Category("C1"),
                new Category("C2"),
                new Category("C3")
            };
            
            if (!_context.Categories.Any())
            {
                _context.Categories.AddRange(categories);
            }
            
            
            if (!_context.Products.Any())
            {
                _context.Products.Add(new Product(categories[1], "Product1", 100, 0));
                _context.Products.Add(new Product(categories[1], "Product2", 500, 0));
                _context.Products.Add(new Product(categories[2], "Product3", 100500, 0));
                _context.Products.Add(new Product(categories[2], "Bestseller1", 200, 0) {PurchaseCount = 11});
                _context.Products.Add(new Product(categories[1], "Bestseller2", 300, 0) {PurchaseCount = 11});
                _context.Products.Add(new Product(categories[1], "Sale1", 400, 10));
                _context.Products.Add(new Product(categories[1], "Sale2", 500, 20));
                await _context.SaveChangesAsync();
            }
        }

        public async Task InitializeAsync()
        {
            await _context.Database.MigrateAsync();
            await SeedEverything();
        }
    }
}