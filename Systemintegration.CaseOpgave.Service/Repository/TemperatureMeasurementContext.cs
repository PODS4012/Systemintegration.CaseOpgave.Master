using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Systemintegration.CaseOpgave.Shared.DataTransferObjects;

namespace Systemintegration.CaseOpgave.Service.Repository
{
    public partial class TemperatureMeasurementContext : DbContext
    {
        public TemperatureMeasurementContext(DbContextOptions options) : base(options) { }

        public virtual DbSet<Temperatur> Temperaturs { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Temperatur>(entity =>
            {
                entity.HasKey(e => new { e.Date, e.Time })
                    .HasName("PK__Temperat__DAF567423CA3787A");

                entity.ToTable("Temperatur");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasColumnName("dato");

                entity.Property(e => e.Time)
                    .HasColumnType("time(0)")
                    .HasColumnName("tidspunkt");

                entity.Property(e => e.Temp)
                    .HasColumnType("numeric(3, 1)")
                    .HasColumnName("grader");
            });

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
