namespace StrongMe.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddMeasurements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Measurements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TraineeId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Chest = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RightArmBiceps = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LeftArmBiceps = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UpWaist = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Waist = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DownWaist = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Hips = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RightThigh = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LeftThigh = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RightCalf = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LeftCalf = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Neck = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Measurements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Measurements_AspNetUsers_TraineeId",
                        column: x => x.TraineeId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Measurements_IsDeleted",
                table: "Measurements",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Measurements_TraineeId",
                table: "Measurements",
                column: "TraineeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Measurements");
        }
    }
}
