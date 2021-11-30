using HeidelbergCement.CaseStudies.Concurrency.Domain.Schedule.Enums;
using HeidelbergCement.CaseStudies.Concurrency.Domain.Schedule.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql;

namespace HeidelbergCement.CaseStudies.Concurrency.Infrastructure.DbContexts.Schedule;

public class ScheduleDbContext: DbContext, IScheduleDbContext
{
    public ScheduleDbContext(DbContextOptions<ScheduleDbContext> options) : base(options)
    {
    }
    public DbSet<Domain.Schedule.Models.Schedule> ScheduleItems { get; set; }
    public DbSet<ScheduleItem> Schedules { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        SetupDateTimeConversion(modelBuilder);
        
        NpgsqlConnection.GlobalTypeMapper.MapEnum<Status>();

        
        modelBuilder.Entity<ScheduleItem>().HasKey(it => it.ScheduleItemId);
        modelBuilder.Entity<ScheduleItem>().Property(x => x.ScheduleId).ValueGeneratedNever();
        modelBuilder.Entity<ScheduleItem>().Property(it => it.Start).IsRequired();
        modelBuilder.Entity<ScheduleItem>().Property(it => it.End).IsRequired();
        modelBuilder.Entity<ScheduleItem>().Property(it => it.IsDeleted).IsRequired();
        modelBuilder.Entity<ScheduleItem>().Property(it => it.AssetId).IsRequired();
        modelBuilder.Entity<ScheduleItem>().Property(it => it.UpdatedOn).IsRequired();

        modelBuilder.Entity<Domain.Schedule.Models.Schedule>().HasKey(it => it.ScheduleId);
        modelBuilder.Entity<Domain.Schedule.Models.Schedule>().Property(it => it.Status).IsRequired();
        modelBuilder.Entity<Domain.Schedule.Models.Schedule>().Property(it => it.PlantCode).IsRequired();
        modelBuilder.Entity<Domain.Schedule.Models.Schedule>().Property(it => it.UpdatedOn).IsRequired();
        

        
        // unique constraint to only allow one draft status schedule per plant
        modelBuilder.Entity<Domain.Schedule.Models.Schedule>()
            .HasIndex(x => new { x.Status, x.PlantCode })
            .IsUnique()
            .HasFilter($"\"Status\" = {(int)Status.Draft}");
        
        Seed(modelBuilder);
    }

    private void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Domain.Schedule.Models.Schedule>().HasData(
            new Domain.Schedule.Models.Schedule
            {
                Status = Status.Draft,
                PlantCode = 1234,
                ScheduleId = 88,
                ScheduleItems = new List<ScheduleItem>(),
                UpdatedOn = new DateTime(2021, 1, 1, 1, 1, 1, DateTimeKind.Utc)
            });
    }
    public void SetModified(object entity)
    {
        Entry(entity).State = EntityState.Modified;
    }
    private void SetupDateTimeConversion(ModelBuilder modelBuilder)
    {
        var dateTimeConverter = new ValueConverter<DateTime, DateTime>(
            v => v.ToUniversalTime(),
            v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

        var nullableDateTimeConverter = new ValueConverter<DateTime?, DateTime?>(
            v => v.HasValue ? v.Value.ToUniversalTime() : v,
            v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : v);

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (entityType.IsKeyless)
            {
                continue;
            }

            foreach (var property in entityType.GetProperties())
            {
                if (property.ClrType == typeof(DateTime))
                {
                    property.SetValueConverter(dateTimeConverter);
                }
                else if (property.ClrType == typeof(DateTime?))
                {
                    property.SetValueConverter(nullableDateTimeConverter);
                }
            }
        }
    }
}