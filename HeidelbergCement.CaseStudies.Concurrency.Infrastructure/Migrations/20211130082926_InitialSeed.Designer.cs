﻿// <auto-generated />
using System;
using HeidelbergCement.CaseStudies.Concurrency.Infrastructure.DbContexts.Schedule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HeidelbergCement.CaseStudies.Concurrency.Infrastructure.Migrations
{
    [DbContext(typeof(ScheduleDbContext))]
    [Migration("20211130082926_InitialSeed")]
    partial class InitialSeed
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("HeidelbergCement.CaseStudies.Concurrency.Domain.Schedule.Models.Schedule", b =>
                {
                    b.Property<int>("ScheduleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ScheduleId"));

                    b.Property<int>("PlantCode")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("ScheduleId");

                    b.HasIndex("Status", "PlantCode")
                        .IsUnique()
                        .HasFilter("\"Status\" = 0");

                    b.ToTable("ScheduleItems");

                    b.HasData(
                        new
                        {
                            ScheduleId = 88,
                            PlantCode = 1234,
                            Status = 0,
                            UpdatedOn = new DateTime(2021, 1, 1, 1, 1, 1, 0, DateTimeKind.Utc)
                        });
                });

            modelBuilder.Entity("HeidelbergCement.CaseStudies.Concurrency.Domain.Schedule.Models.ScheduleItem", b =>
                {
                    b.Property<int>("ScheduleItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ScheduleItemId"));

                    b.Property<int>("AssetId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("End")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<int>("ScheduleId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Start")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("UpdatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("ScheduleItemId");

                    b.HasIndex("ScheduleId");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("HeidelbergCement.CaseStudies.Concurrency.Domain.Schedule.Models.ScheduleItem", b =>
                {
                    b.HasOne("HeidelbergCement.CaseStudies.Concurrency.Domain.Schedule.Models.Schedule", "Schedule")
                        .WithMany("ScheduleItems")
                        .HasForeignKey("ScheduleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Schedule");
                });

            modelBuilder.Entity("HeidelbergCement.CaseStudies.Concurrency.Domain.Schedule.Models.Schedule", b =>
                {
                    b.Navigation("ScheduleItems");
                });
#pragma warning restore 612, 618
        }
    }
}
