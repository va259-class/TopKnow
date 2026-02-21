using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TopKnow.Data.Migrations
{
    /// <inheritdoc />
    public partial class PostUserColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                schema: "Communication",
                table: "Posts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UserId",
                schema: "Communication",
                table: "Posts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Users_UserId",
                schema: "Communication",
                table: "Posts",
                column: "UserId",
                principalSchema: "Main",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Users_UserId",
                schema: "Communication",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_UserId",
                schema: "Communication",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "Communication",
                table: "Posts");
        }
    }
}
