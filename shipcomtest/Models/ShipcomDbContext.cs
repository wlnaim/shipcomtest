using System;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace shipcomtest.Models
{
    public partial class ShipcomDbContext : DbContext
    {

       public ShipcomDbContext(DbContextOptions<ShipcomDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ColorConfiguration> ColorConfigurations { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<ColorConfiguration>(entity =>
            {
                entity.ToTable("ColorConfiguration");

                entity.Property(e => e.Color)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Tolerance).HasColumnType("decimal(4, 3)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
