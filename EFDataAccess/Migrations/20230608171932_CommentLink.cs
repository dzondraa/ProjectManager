using Microsoft.EntityFrameworkCore.Migrations;

namespace EFDataAccess.Migrations
{
    public partial class CommentLink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WorkItemId",
                table: "Comments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_WorkItemId",
                table: "Comments",
                column: "WorkItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_WorkItems_WorkItemId",
                table: "Comments",
                column: "WorkItemId",
                principalTable: "WorkItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_WorkItems_WorkItemId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_WorkItemId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "WorkItemId",
                table: "Comments");
        }
    }
}
