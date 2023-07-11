using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ELTE.TodoList.Persistence.Migrations
{
    public partial class AddImageToItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Items",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Items");
        }
    }
}
