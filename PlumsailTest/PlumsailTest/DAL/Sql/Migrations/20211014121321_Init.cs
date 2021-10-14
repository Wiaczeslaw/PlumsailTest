using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PlumsailTest.DAL.Sql.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FormTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FormItemTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FormTemplateId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormItemTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormItemTemplates_FormTemplates_FormTemplateId",
                        column: x => x.FormTemplateId,
                        principalTable: "FormTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Forms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FormTemplateId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Forms_FormTemplates_FormTemplateId",
                        column: x => x.FormTemplateId,
                        principalTable: "FormTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FormItemSelectValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FormItemTemplateId = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormItemSelectValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormItemSelectValues_FormItemTemplates_FormItemTemplateId",
                        column: x => x.FormItemTemplateId,
                        principalTable: "FormItemTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FormItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FormItemTemplateId = table.Column<Guid>(type: "uuid", nullable: false),
                    FormId = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true),
                    FormItemSelectValueId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormItems_FormItemSelectValues_FormItemSelectValueId",
                        column: x => x.FormItemSelectValueId,
                        principalTable: "FormItemSelectValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FormItems_FormItemTemplates_FormItemTemplateId",
                        column: x => x.FormItemTemplateId,
                        principalTable: "FormItemTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FormItems_Forms_FormId",
                        column: x => x.FormId,
                        principalTable: "Forms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FormItems_FormId",
                table: "FormItems",
                column: "FormId");

            migrationBuilder.CreateIndex(
                name: "IX_FormItems_FormItemSelectValueId",
                table: "FormItems",
                column: "FormItemSelectValueId");

            migrationBuilder.CreateIndex(
                name: "IX_FormItems_FormItemTemplateId",
                table: "FormItems",
                column: "FormItemTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_FormItemSelectValues_FormItemTemplateId",
                table: "FormItemSelectValues",
                column: "FormItemTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_FormItemTemplates_FormTemplateId",
                table: "FormItemTemplates",
                column: "FormTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Forms_FormTemplateId",
                table: "Forms",
                column: "FormTemplateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FormItems");

            migrationBuilder.DropTable(
                name: "FormItemSelectValues");

            migrationBuilder.DropTable(
                name: "Forms");

            migrationBuilder.DropTable(
                name: "FormItemTemplates");

            migrationBuilder.DropTable(
                name: "FormTemplates");
        }
    }
}
