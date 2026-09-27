using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Book_A_Doc.Infrastructre.Migrations
{
    /// <inheritdoc />
    public partial class InheritFromBaseEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Holidays");

            migrationBuilder.DropIndex(
                name: "IX_DoctorExceptions_DoctorId_Date",
                table: "DoctorExceptions");

            migrationBuilder.AlterColumn<Guid>(
                name: "DoctorId",
                table: "DoctorExceptions",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "DoctorAvailabilities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "DoctorAvailabilities",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "DoctorAvailabilities",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "DoctorAvailabilities",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DoctorExceptions_Date",
                table: "DoctorExceptions",
                column: "Date",
                unique: true,
                filter: "[DoctorId] IS NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorExceptions_DoctorId_Date",
                table: "DoctorExceptions",
                columns: new[] { "DoctorId", "Date" },
                unique: true,
                filter: "[DoctorId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DoctorExceptions_Date",
                table: "DoctorExceptions");

            migrationBuilder.DropIndex(
                name: "IX_DoctorExceptions_DoctorId_Date",
                table: "DoctorExceptions");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "DoctorAvailabilities");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "DoctorAvailabilities");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "DoctorAvailabilities");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "DoctorAvailabilities");

            migrationBuilder.AlterColumn<Guid>(
                name: "DoctorId",
                table: "DoctorExceptions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Holidays",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Holidays", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoctorExceptions_DoctorId_Date",
                table: "DoctorExceptions",
                columns: new[] { "DoctorId", "Date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Holidays_Date",
                table: "Holidays",
                column: "Date",
                unique: true);
        }
    }
}
