using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CyberQuiz.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddAttemptIdToUserResult : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AttemptId",
                table: "UserResults",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AttemptId",
                table: "UserResults");
        }
    }
}
