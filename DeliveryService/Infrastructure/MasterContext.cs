using DeliveryService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Xml;

namespace DeliveryService.Infrastructure
{
    public class MasterContext : DbContext
    {
        public MasterContext(DbContextOptions<MasterContext> options) : base(options)
        {
        }

        public virtual DbSet<DeliveryRequest> DeliveryRequest { get; set; }
        public virtual DbSet<Driver> Driver { get; set; }
        public virtual DbSet<DriverDelivery> DriverDelivery { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasPostgresExtension("uuid-ossp");

            modelBuilder.Entity<DeliveryRequest>(entity =>
            {
                entity.Property(e => e.CreateTime)
                    .HasColumnType("timestamp without time zone")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.Id)
                    .HasDefaultValueSql("uuid_generate_v4()");
                entity.Property(e => e.Status)
                   .HasDefaultValue("Pending");
            });

            modelBuilder.Entity<Driver>(entity =>
            {
                entity.Property(e => e.CreateTime)
                    .HasColumnType("timestamp without time zone")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.Id)
                    .HasDefaultValueSql("uuid_generate_v4()");
                entity.Property(e => e.AvailabilityStatus)
                    .HasDefaultValue("Online");
            });

            modelBuilder.Entity<DriverDelivery>(entity =>
            {
                entity.Property(e => e.CreateTime)
                    .HasColumnType("timestamp without time zone")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.Id)
                    .HasDefaultValueSql("uuid_generate_v4()");
                entity.Property(e => e.DeliverStatus)
                 .HasDefaultValue("PickedUp");
            });
        }
    }
}