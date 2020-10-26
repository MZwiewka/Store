using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.Migrations
{
    public partial class x : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpecificationFieldValue_Products_ProductID",
                table: "SpecificationFieldValue");

            migrationBuilder.DropForeignKey(
                name: "FK_SpecificationFieldValue_SpecificationFields_SpecificationFieldID",
                table: "SpecificationFieldValue");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SpecificationFieldValue",
                table: "SpecificationFieldValue");

            migrationBuilder.RenameTable(
                name: "SpecificationFieldValue",
                newName: "SpecificationFieldValues");

            migrationBuilder.RenameIndex(
                name: "IX_SpecificationFieldValue_SpecificationFieldID",
                table: "SpecificationFieldValues",
                newName: "IX_SpecificationFieldValues_SpecificationFieldID");

            migrationBuilder.RenameIndex(
                name: "IX_SpecificationFieldValue_ProductID",
                table: "SpecificationFieldValues",
                newName: "IX_SpecificationFieldValues_ProductID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SpecificationFieldValues",
                table: "SpecificationFieldValues",
                column: "SpecificationFieldValueID");

            migrationBuilder.AddForeignKey(
                name: "FK_SpecificationFieldValues_Products_ProductID",
                table: "SpecificationFieldValues",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SpecificationFieldValues_SpecificationFields_SpecificationFieldID",
                table: "SpecificationFieldValues",
                column: "SpecificationFieldID",
                principalTable: "SpecificationFields",
                principalColumn: "SpecificationFieldID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpecificationFieldValues_Products_ProductID",
                table: "SpecificationFieldValues");

            migrationBuilder.DropForeignKey(
                name: "FK_SpecificationFieldValues_SpecificationFields_SpecificationFieldID",
                table: "SpecificationFieldValues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SpecificationFieldValues",
                table: "SpecificationFieldValues");

            migrationBuilder.RenameTable(
                name: "SpecificationFieldValues",
                newName: "SpecificationFieldValue");

            migrationBuilder.RenameIndex(
                name: "IX_SpecificationFieldValues_SpecificationFieldID",
                table: "SpecificationFieldValue",
                newName: "IX_SpecificationFieldValue_SpecificationFieldID");

            migrationBuilder.RenameIndex(
                name: "IX_SpecificationFieldValues_ProductID",
                table: "SpecificationFieldValue",
                newName: "IX_SpecificationFieldValue_ProductID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SpecificationFieldValue",
                table: "SpecificationFieldValue",
                column: "SpecificationFieldValueID");

            migrationBuilder.AddForeignKey(
                name: "FK_SpecificationFieldValue_Products_ProductID",
                table: "SpecificationFieldValue",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SpecificationFieldValue_SpecificationFields_SpecificationFieldID",
                table: "SpecificationFieldValue",
                column: "SpecificationFieldID",
                principalTable: "SpecificationFields",
                principalColumn: "SpecificationFieldID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
