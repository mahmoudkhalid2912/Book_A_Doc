using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Book_A_Doc.Infrastructre.Migrations
{
    /// <inheritdoc />
    public partial class AddDoctorFullNameProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "Doctors");
        }
    }
}
