using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Services.Migrations
{
    /// <inheritdoc />
    public partial class AddNullableLocalIdToCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LocalId",
                table: "Categories",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_LocalId",
                table: "Categories",
                column: "LocalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Locals_LocalId",
                table: "Categories",
                column: "LocalId",
                principalTable: "Locals",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Locals_LocalId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_LocalId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "LocalId",
                table: "Categories");
        }
    }
}
