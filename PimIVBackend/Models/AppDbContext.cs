using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;
using PimIVBackend.Models.Base;

namespace PimIVBackend.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Entity>()
                .ToTable("Entity");
            modelBuilder.Entity<EntityGuest>()
                .ToTable("Entity");
            modelBuilder.Entity<EntityCompany>()
                .ToTable("Entity");
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetDefaults();

            return base.SaveChangesAsync(cancellationToken);

        }

        private void SetDefaults()
        {
            var properties = ChangeTracker.Entries<ModelBase>()
                .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified)
                .ToList();

            properties.ForEach(x =>
            {
                if(x.State == EntityState.Added)
                    x.Entity.DateAdd = DateTime.Now;

                x.Entity.DateUp = DateTime.Now;
            });
        }

        public DbSet<Entity> Entities { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Room> Rooms { get; set; }
    }
}
