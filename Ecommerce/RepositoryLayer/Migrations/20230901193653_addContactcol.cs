using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepositoryLayer.Migrations
{
    public partial class addContactcol : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Unvan",
                table: "Contacts",
                newName: "Message");

            migrationBuilder.AddColumn<string>(
                name: "Fullname",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fullname",
                table: "Contacts");

            migrationBuilder.RenameColumn(
                name: "Message",
                table: "Contacts",
                newName: "Unvan");
        }
    }
}
