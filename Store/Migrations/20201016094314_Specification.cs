using Microsoft.EntityFrameworkCore.Migrations;

namespace Store.Migrations
{
    public partial class Specification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpecificationFieldValue_SpecificationField_SpecificationFieldID",
                table: "SpecificationFieldValue");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SpecificationField",
                table: "SpecificationField");

            migrationBuilder.RenameTable(
                name: "SpecificationField",
                newName: "SpecificationFields");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SpecificationFields",
                table: "SpecificationFields",
                column: "SpecificationFieldID");

            migrationBuilder.AddForeignKey(
                name: "FK_SpecificationFieldValue_SpecificationFields_SpecificationFieldID",
                table: "SpecificationFieldValue",
                column: "SpecificationFieldID",
                principalTable: "SpecificationFields",
                principalColumn: "SpecificationFieldID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpecificationFieldValue_SpecificationFields_SpecificationFieldID",
                table: "SpecificationFieldValue");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SpecificationFields",
                table: "SpecificationFields");

            migrationBuilder.RenameTable(
                name: "SpecificationFields",
                newName: "SpecificationField");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SpecificationField",
                table: "SpecificationField",
                column: "SpecificationFieldID");

            migrationBuilder.AddForeignKey(
                name: "FK_SpecificationFieldValue_SpecificationField_SpecificationFieldID",
                table: "SpecificationFieldValue",
                column: "SpecificationFieldID",
                principalTable: "SpecificationField",
                principalColumn: "SpecificationFieldID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
