using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TopKnow.Data.Migrations
{
    /// <inheritdoc />
    public partial class MatchHistoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MatchHistories",
                schema: "Game",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MatchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WinnerUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchHistories_Matches_MatchId",
                        column: x => x.MatchId,
                        principalSchema: "Game",
                        principalTable: "Matches",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MatchHistories_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalSchema: "Game",
                        principalTable: "Questions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MatchHistories_Users_WinnerUserId",
                        column: x => x.WinnerUserId,
                        principalSchema: "Main",
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MatchHistories_MatchId",
                schema: "Game",
                table: "MatchHistories",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchHistories_QuestionId",
                schema: "Game",
                table: "MatchHistories",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchHistories_WinnerUserId",
                schema: "Game",
                table: "MatchHistories",
                column: "WinnerUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MatchHistories",
                schema: "Game");
        }
    }
}
