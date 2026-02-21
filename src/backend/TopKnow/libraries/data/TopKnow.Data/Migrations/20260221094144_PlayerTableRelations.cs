using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TopKnow.Data.Migrations
{
    /// <inheritdoc />
    public partial class PlayerTableRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Users_LeftUserId",
                schema: "Game",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Users_RightUserId",
                schema: "Game",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Users_WinnerUserId",
                schema: "Game",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_MatchHistories_Users_WinnerUserId",
                schema: "Game",
                table: "MatchHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_ScoreBoard_Users_UserId",
                schema: "Game",
                table: "ScoreBoard");

            migrationBuilder.RenameTable(
                name: "Players",
                newName: "Players",
                newSchema: "Game");

            migrationBuilder.RenameColumn(
                name: "UserId",
                schema: "Game",
                table: "ScoreBoard",
                newName: "PlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_ScoreBoard_UserId",
                schema: "Game",
                table: "ScoreBoard",
                newName: "IX_ScoreBoard_PlayerId");

            migrationBuilder.RenameColumn(
                name: "WinnerUserId",
                schema: "Game",
                table: "MatchHistories",
                newName: "WinnerPlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_MatchHistories_WinnerUserId",
                schema: "Game",
                table: "MatchHistories",
                newName: "IX_MatchHistories_WinnerPlayerId");

            migrationBuilder.RenameColumn(
                name: "WinnerUserId",
                schema: "Game",
                table: "Matches",
                newName: "WinnerPlayerId");

            migrationBuilder.RenameColumn(
                name: "RightUserId",
                schema: "Game",
                table: "Matches",
                newName: "RightPlayerId");

            migrationBuilder.RenameColumn(
                name: "LeftUserId",
                schema: "Game",
                table: "Matches",
                newName: "LeftPlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_Matches_WinnerUserId",
                schema: "Game",
                table: "Matches",
                newName: "IX_Matches_WinnerPlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_Matches_RightUserId",
                schema: "Game",
                table: "Matches",
                newName: "IX_Matches_RightPlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_Matches_LeftUserId",
                schema: "Game",
                table: "Matches",
                newName: "IX_Matches_LeftPlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_UserId",
                schema: "Game",
                table: "Players",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Players_LeftPlayerId",
                schema: "Game",
                table: "Matches",
                column: "LeftPlayerId",
                principalSchema: "Game",
                principalTable: "Players",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Players_RightPlayerId",
                schema: "Game",
                table: "Matches",
                column: "RightPlayerId",
                principalSchema: "Game",
                principalTable: "Players",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Players_WinnerPlayerId",
                schema: "Game",
                table: "Matches",
                column: "WinnerPlayerId",
                principalSchema: "Game",
                principalTable: "Players",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MatchHistories_Players_WinnerPlayerId",
                schema: "Game",
                table: "MatchHistories",
                column: "WinnerPlayerId",
                principalSchema: "Game",
                principalTable: "Players",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Users_UserId",
                schema: "Game",
                table: "Players",
                column: "UserId",
                principalSchema: "Main",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ScoreBoard_Players_PlayerId",
                schema: "Game",
                table: "ScoreBoard",
                column: "PlayerId",
                principalSchema: "Game",
                principalTable: "Players",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Players_LeftPlayerId",
                schema: "Game",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Players_RightPlayerId",
                schema: "Game",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Players_WinnerPlayerId",
                schema: "Game",
                table: "Matches");

            migrationBuilder.DropForeignKey(
                name: "FK_MatchHistories_Players_WinnerPlayerId",
                schema: "Game",
                table: "MatchHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_Players_Users_UserId",
                schema: "Game",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_ScoreBoard_Players_PlayerId",
                schema: "Game",
                table: "ScoreBoard");

            migrationBuilder.DropIndex(
                name: "IX_Players_UserId",
                schema: "Game",
                table: "Players");

            migrationBuilder.RenameTable(
                name: "Players",
                schema: "Game",
                newName: "Players");

            migrationBuilder.RenameColumn(
                name: "PlayerId",
                schema: "Game",
                table: "ScoreBoard",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ScoreBoard_PlayerId",
                schema: "Game",
                table: "ScoreBoard",
                newName: "IX_ScoreBoard_UserId");

            migrationBuilder.RenameColumn(
                name: "WinnerPlayerId",
                schema: "Game",
                table: "MatchHistories",
                newName: "WinnerUserId");

            migrationBuilder.RenameIndex(
                name: "IX_MatchHistories_WinnerPlayerId",
                schema: "Game",
                table: "MatchHistories",
                newName: "IX_MatchHistories_WinnerUserId");

            migrationBuilder.RenameColumn(
                name: "WinnerPlayerId",
                schema: "Game",
                table: "Matches",
                newName: "WinnerUserId");

            migrationBuilder.RenameColumn(
                name: "RightPlayerId",
                schema: "Game",
                table: "Matches",
                newName: "RightUserId");

            migrationBuilder.RenameColumn(
                name: "LeftPlayerId",
                schema: "Game",
                table: "Matches",
                newName: "LeftUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Matches_WinnerPlayerId",
                schema: "Game",
                table: "Matches",
                newName: "IX_Matches_WinnerUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Matches_RightPlayerId",
                schema: "Game",
                table: "Matches",
                newName: "IX_Matches_RightUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Matches_LeftPlayerId",
                schema: "Game",
                table: "Matches",
                newName: "IX_Matches_LeftUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Users_LeftUserId",
                schema: "Game",
                table: "Matches",
                column: "LeftUserId",
                principalSchema: "Main",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Users_RightUserId",
                schema: "Game",
                table: "Matches",
                column: "RightUserId",
                principalSchema: "Main",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Users_WinnerUserId",
                schema: "Game",
                table: "Matches",
                column: "WinnerUserId",
                principalSchema: "Main",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MatchHistories_Users_WinnerUserId",
                schema: "Game",
                table: "MatchHistories",
                column: "WinnerUserId",
                principalSchema: "Main",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ScoreBoard_Users_UserId",
                schema: "Game",
                table: "ScoreBoard",
                column: "UserId",
                principalSchema: "Main",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
