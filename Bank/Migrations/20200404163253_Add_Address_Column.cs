using Microsoft.EntityFrameworkCore.Migrations;

namespace Bank.Migrations
{
    public partial class Add_Address_Column : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Number",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostCode",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Banks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Banks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Number",
                table: "Banks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostCode",
                table: "Banks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "Banks",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "PostCode",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Banks");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Banks");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "Banks");

            migrationBuilder.DropColumn(
                name: "PostCode",
                table: "Banks");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "Banks");
        }
    }
}
