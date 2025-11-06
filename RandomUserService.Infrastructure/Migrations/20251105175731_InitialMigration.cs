using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RandomUserService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Name_Title = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Name_First = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Name_Last = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Location_Street_Number = table.Column<int>(type: "int", nullable: false),
                    Location_Street_Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Location_City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Location_State = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Location_Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Location_PostCode = table.Column<int>(type: "int", nullable: false),
                    Location_Latitude = table.Column<double>(type: "float", nullable: false),
                    Location_Longitude = table.Column<double>(type: "float", nullable: false),
                    Location_TimeZone_Offset = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Location_TimeZone_Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DateOfBirth_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateOfBirth_Age = table.Column<int>(type: "int", nullable: false),
                    Registered_Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Registered_Age = table.Column<int>(type: "int", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Cell = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ExternalId_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ExternalId_Value = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Picture_Large = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Picture_Medium = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Picture_Thumbnail = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Nat = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
