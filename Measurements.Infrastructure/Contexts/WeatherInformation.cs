using Measurements.Domain;
using Microsoft.EntityFrameworkCore;

namespace Measurements.Infrastructure.Contexts
{
    public partial class WeatherInformation : DbContext
    {
        public WeatherInformation(DbContextOptions<WeatherInformation> options) : base(options)
        {
            this.Database.SetCommandTimeout(200);
        }

        public virtual DbSet<MeasuredData> WeatherInfo { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MeasuredData>(entity =>
            {
                entity.HasKey(e => new { e.DeviceId, e.SensorType });
                entity.ToTable("WeatherData");
                entity.Property(e => e.DeviceId).HasColumnName("DEVICE_ID").HasMaxLength(20).IsUnicode(false);
                entity.Property(e => e.SensorType).HasColumnName("SENSOR_TYPE").HasMaxLength(20).IsUnicode(false);
                entity.Property(e => e.MeasureDate).HasColumnName("MEASURE_DATE").HasColumnType("datetime");
                entity.Property(e => e.Temperature).HasColumnName("TEMPERATURE");
                entity.Property(e => e.Humidity).HasColumnName("HUMIDITY");
                entity.Property(e => e.Rainfall).HasColumnName("RAINFALL");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
