namespace StrongMe.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddPersonalPrograms : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Notes",
                table: "TemplatePrograms");

            migrationBuilder.CreateTable(
                name: "PersonalPrograms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TemplateProgramId = table.Column<int>(type: "int", nullable: false),
                    TraineeId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalPrograms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonalPrograms_AspNetUsers_TraineeId",
                        column: x => x.TraineeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonalPrograms_TemplatePrograms_TemplateProgramId",
                        column: x => x.TemplateProgramId,
                        principalTable: "TemplatePrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonalPrograms_IsDeleted",
                table: "PersonalPrograms",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalPrograms_TemplateProgramId",
                table: "PersonalPrograms",
                column: "TemplateProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalPrograms_TraineeId",
                table: "PersonalPrograms",
                column: "TraineeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonalPrograms");

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "TemplatePrograms",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
