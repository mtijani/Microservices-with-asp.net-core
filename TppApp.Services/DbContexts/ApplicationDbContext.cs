using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TppApp.Services.ProductAPI.models;

namespace TppApp.Services.ProductAPI.DbContexts
{
    public class ApplicationDbContext : DbContext 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet <Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 1,
                Name = "Chorba",
                Price = 15,
                Description = " Delicious and delightful  .<br/>Spicy Tunisian Chorba",
                ImageUrl = "https://dotnettp.blob.core.windows.net/food/Chorba.jpg",
                CategoryName = "Appetizer"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 2,
                Name = "Tajine",
                Price = 13.99,
                Description = "North african Tajine with eggs, cheese and meat ",
                ImageUrl = "https://dotnettp.blob.core.windows.net/food/tajine.jpg",
                CategoryName = "Appetizer"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 3,
                Name = "Baklawa",
                Price = 10.99,
                Description = "authentic baklava",
                ImageUrl = "https://dotnettp.blob.core.windows.net/food/Baklawa-Tunisienne.jpg",
                CategoryName = "Dessert"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 4,
                Name = "Kouskous",
                Price = 15,
                Description = "Delicious kouskous with vegetables and fish",
                ImageUrl = "https://dotnettp.blob.core.windows.net/food/couscous-tunisien.jpeg",
                CategoryName = "Entree"
            });
        }
    }
}
