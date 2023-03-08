using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodeNotion.Academy.OrderScheduling.Migrations
{
    /// <inheritdoc />
    public partial class ordertable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Customer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order_number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cutting_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Preparation_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Bending_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Assembly_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
