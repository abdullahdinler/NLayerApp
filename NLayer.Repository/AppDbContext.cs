using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NLayer.Core.Models;

namespace NLayer.Repository
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        // SaveChanges() ve SaveChangesAsync() metodlarını override ederek tüm entityler için CreatedDate ve UpdatedDate alanlarını otomatik olarak güncelleyebiliriz.
        public override int SaveChanges()
        {
            UpdateChangeTracker();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateChangeTracker();
            return base.SaveChangesAsync(cancellationToken);
        }

        public void UpdateChangeTracker()
        {
            foreach (var item in ChangeTracker.Entries())
            {
                if (item.Entity is BaseEntity entityReference)
                {
                    switch (item.State)
                    {
                        case EntityState.Added:
                            Entry(entityReference).Property(x => x.UpdatedDate).IsModified = false;
                            entityReference.CreatedDate = DateTime.Now;
                            break;
                        case EntityState.Modified:
                            Entry(entityReference).Property(x => x.CreatedDate).IsModified = false;
                            entityReference.UpdatedDate = DateTime.Now;
                            break;
                    }
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Assembly (NLayer.Repository) içinde olan veya yazdığımız tüm configuration bulup uygulanmasını sağlar.
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }
    }
}
