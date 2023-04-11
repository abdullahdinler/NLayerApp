using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core;

namespace NLayer.Repository.Configurations
{
    // Burada ProductFeature entitysi için veritabanı configuration ayarı yapılmaktadır. (Primer key, Karekter uzunluğu, zorunlu alan vs.)
    internal class ProductFeatureConfiguration : IEntityTypeConfiguration<ProductFeature>
    {
        public void Configure(EntityTypeBuilder<ProductFeature> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            //Product ile ProductFeature entitysi arasındaki ilişki belirlendi. (Bire bir ilişki)
            builder.HasOne(x => x.Product).WithOne(x => x.ProductFeature)
                .HasForeignKey<ProductFeature>(x => x.ProductId);
        }
    }
}
