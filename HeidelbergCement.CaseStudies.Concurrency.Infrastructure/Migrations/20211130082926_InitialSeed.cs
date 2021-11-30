using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HeidelbergCement.CaseStudies.Concurrency.Infrastructure.Migrations
{
    public partial class InitialSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ScheduleItems",
                columns: new[] { "ScheduleId", "PlantCode", "Status", "UpdatedOn" },
                values: new object[] { 88, 1234, 0, new DateTime(2021, 1, 1, 1, 1, 1, 0, DateTimeKind.Utc) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ScheduleItems",
                keyColumn: "ScheduleId",
                keyValue: 88);
        }
    }
}
