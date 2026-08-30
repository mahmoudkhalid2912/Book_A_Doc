using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Book_A_Doc.Infrastructre.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUnImportantCoulmn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "DoctorExceptions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "DoctorExceptions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
