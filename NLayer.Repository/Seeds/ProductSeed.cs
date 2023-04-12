using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core.Models;

namespace NLayer.Repository.Seeds
{
    internal class ProductSeed : IEntityTypeConfiguration<Product>
    {
        // Veri tabanı oluşturulduğu zaman otomatik olarak eklenecek veriler yazıldı.
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(
                new Product
                {
                    Id = 1, CategoryId = 1, Name = "Kalem 1", Price = 100, Stock = 34, CreatedDate = DateTime.Now
                },
                new Product
                {
                    Id = 2, CategoryId = 1, Name = "Kalem 2", Price = 200, Stock = 37, CreatedDate = DateTime.Now
                },
                new Product
                {
                    Id = 3, CategoryId = 1, Name = "Kalem 3", Price = 300, Stock = 39, CreatedDate = DateTime.Now
                },
                new Product
                {
                    Id = 4, CategoryId = 2, Name = "Defter 1", Price = 100, Stock = 34, CreatedDate = DateTime.Now
                },
                new Product
                {
                    Id = 5, CategoryId = 2, Name = "Defter 2", Price = 100, Stock = 34, CreatedDate = DateTime.Now
                },
                new Product
                {
                    Id = 6, CategoryId = 2, Name = "Defter 3", Price = 100, Stock = 34, CreatedDate = DateTime.Now
                },
                new Product
                {
                    Id = 7, CategoryId = 3, Name = "Kitap 1", Price = 100, Stock = 34, CreatedDate = DateTime.Now
                }, new Product
                {
                    Id = 8, CategoryId = 3, Name = "Kitap 2", Price = 100, Stock = 34, CreatedDate = DateTime.Now
                },
                new Product
                {
                    Id = 9, CategoryId = 3, Name = "Kitap 3", Price = 100, Stock = 34, CreatedDate = DateTime.Now
                }
            );
        }
    }
}
