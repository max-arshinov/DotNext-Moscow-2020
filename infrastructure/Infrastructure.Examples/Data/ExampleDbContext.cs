using System;
using Infrastructure.Examples.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Examples.Data
{
    public class ExampleDbContext: DbContext
    {
        public DbSet<Product> Products { get; protected set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
        }
    }
}