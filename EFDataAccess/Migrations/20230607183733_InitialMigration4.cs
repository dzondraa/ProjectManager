using Microsoft.EntityFrameworkCore.Migrations;

namespace EFDataAccess.Migrations
{
    public partial class InitialMigration4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkItems_WorkItemTypes_TypeId",
                table: "WorkItems");

            migrationBuilder.DropIndex(
                name: "IX_WorkItems_TypeId",
                table: "WorkItems");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "WorkItems");

            migrationBuilder.AddColumn<int>(
                name: "WorkItemTypeId",
                table: "WorkItems",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkItems_WorkItemTypeId",
                table: "WorkItems",
                column: "WorkItemTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkItems_WorkItemTypes_WorkItemTypeId",
                table: "WorkItems",
                column: "WorkItemTypeId",
                principalTable: "WorkItemTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkItems_WorkItemTypes_WorkItemTypeId",
                table: "WorkItems");

            migrationBuilder.DropIndex(
                name: "IX_WorkItems_WorkItemTypeId",
                table: "WorkItems");

            migrationBuilder.DropColumn(
                name: "WorkItemTypeId",
                table: "WorkItems");

            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "WorkItems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkItems_TypeId",
                table: "WorkItems",
                column: "TypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkItems_WorkItemTypes_TypeId",
                table: "WorkItems",
                column: "TypeId",
                principalTable: "WorkItemTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
