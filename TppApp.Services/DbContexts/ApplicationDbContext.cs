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
                ImageUrl = "https://micservicestp.blob.core.windows.net/tpmicroservices/Chorba.jpeg",
                CategoryName = "Appetizer"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 2,
                Name = "Tajine",
                Price = 13.99,
                Description = "North african Tajine with eggs, cheese and meat ",
                ImageUrl = "https://micservicestp.blob.core.windows.net/tpmicroservices/Tajine.jpeg",
                CategoryName = "Appetizer"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 3,
                Name = "Baklava",
                Price = 10.99,
                Description = "authentic baklava",
                ImageUrl = "https://micservicestp.blob.core.windows.net/tpmicroservices/Baklava.jpeg",
                CategoryName = "Dessert"
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 4,
                Name = "Kouskous",
                Price = 15,
                Description = "Delicious kouskous with vegetables and fish",
                ImageUrl = "https://micservicestp.blob.core.windows.net/tpmicroservices/kouskous.jpeg",
                CategoryName = "Entree"
            });
        }
    }
}
