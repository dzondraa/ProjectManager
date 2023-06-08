using Microsoft.EntityFrameworkCore.Migrations;

namespace EFDataAccess.Migrations
{
    public partial class InitialMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkItems_Projects_ProjectId1",
                table: "WorkItems");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkItems_WorkItemTypes_WorkItemTypeId",
                table: "WorkItems");

            migrationBuilder.DropIndex(
                name: "IX_WorkItems_ProjectId1",
                table: "WorkItems");

            migrationBuilder.DropIndex(
                name: "IX_WorkItems_WorkItemTypeId",
                table: "WorkItems");

            migrationBuilder.DropColumn(
                name: "ProjectId1",
                table: "WorkItems");

            migrationBuilder.DropColumn(
                name: "WorkItemTypeId",
                table: "WorkItems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProjectId1",
                table: "WorkItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WorkItemTypeId",
                table: "WorkItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkItems_ProjectId1",
                table: "WorkItems",
                column: "ProjectId1");

            migrationBuilder.CreateIndex(
                name: "IX_WorkItems_WorkItemTypeId",
                table: "WorkItems",
                column: "WorkItemTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkItems_Projects_ProjectId1",
                table: "WorkItems",
                column: "ProjectId1",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkItems_WorkItemTypes_WorkItemTypeId",
                table: "WorkItems",
                column: "WorkItemTypeId",
                principalTable: "WorkItemTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
