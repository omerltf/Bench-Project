using AirBench.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace AirBench.Data
{
    public class Context : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Bench> Benches { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            var benchEntity = modelBuilder.Entity<Bench>();
            benchEntity.Property(b => b.Latitude).HasPrecision(9, 6);
            benchEntity.Property(b => b.Longitude).HasPrecision(9, 6);

            base.OnModelCreating(modelBuilder);
        }
    }
}