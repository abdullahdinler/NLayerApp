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
    internal class ProductFeatureSeed : IEntityTypeConfiguration<ProductFeature>
    {
        // Veri tabanı oluşturulduğu zaman otomatik olarak eklenecek veriler yazıldı.
        public void Configure(EntityTypeBuilder<ProductFeature> builder)
        {
            builder.HasData(
                new ProductFeature { Id = 1, ProductId = 1, Color = "Red", Height = 100, Width = 150 },
                new ProductFeature { Id = 2, ProductId = 1, Color = "White", Height = 200, Width = 150 },
                new ProductFeature { Id = 3, ProductId = 1, Color = "Purple", Height = 300, Width = 150 },
                new ProductFeature { Id = 4, ProductId = 2, Color = "Blue", Height = 400, Width = 150 },
                new ProductFeature { Id = 5, ProductId = 2, Color = "Green", Height = 100, Width = 150 },
                new ProductFeature { Id = 6, ProductId = 2, Color = "Yellow", Height = 100, Width = 150 },
                new ProductFeature { Id = 7, ProductId = 3, Color = "Black", Height = 100, Width = 150 },
                new ProductFeature { Id = 8, ProductId = 3, Color = "Pink", Height = 100, Width = 150 },
                new ProductFeature { Id = 9, ProductId = 3, Color = "Grey", Height = 100, Width = 150 }
            );
        }
    }
}
