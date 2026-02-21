using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TopKnow.Data.Migrations
{
    /// <inheritdoc />
    public partial class QuestionType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TypeId",
                schema: "Game",
                table: "Questions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Questions_TypeId",
                schema: "Game",
                table: "Questions",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_LookUps_TypeId",
                schema: "Game",
                table: "Questions",
                column: "TypeId",
                principalSchema: "Main",
                principalTable: "LookUps",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_LookUps_TypeId",
                schema: "Game",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_TypeId",
                schema: "Game",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "TypeId",
                schema: "Game",
                table: "Questions");
        }
    }
}
