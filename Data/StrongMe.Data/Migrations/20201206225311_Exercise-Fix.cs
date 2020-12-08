namespace StrongMe.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class ExerciseFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Image_Excercises_ExcerciseId",
                table: "Image");

            migrationBuilder.DropTable(
                name: "Excercises");

            migrationBuilder.RenameColumn(
                name: "ExcerciseId",
                table: "Image",
                newName: "ExerciseId");

            migrationBuilder.RenameIndex(
                name: "IX_Image_ExcerciseId",
                table: "Image",
                newName: "IX_Image_ExerciseId");

            migrationBuilder.CreateTable(
                name: "Exercises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrainerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    BodyPartId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exercises_AspNetUsers_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Exercises_BodyParts_BodyPartId",
                        column: x => x.BodyPartId,
                        principalTable: "BodyParts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Exercises_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_BodyPartId",
                table: "Exercises",
                column: "BodyPartId");

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_CategoryId",
                table: "Exercises",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_IsDeleted",
                table: "Exercises",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_TrainerId",
                table: "Exercises",
                column: "TrainerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Image_Exercises_ExerciseId",
                table: "Image",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Image_Exercises_ExerciseId",
                table: "Image");

            migrationBuilder.DropTable(
                name: "Exercises");

            migrationBuilder.RenameColumn(
                name: "ExerciseId",
                table: "Image",
                newName: "ExcerciseId");

            migrationBuilder.RenameIndex(
                name: "IX_Image_ExerciseId",
                table: "Image",
                newName: "IX_Image_ExcerciseId");

            migrationBuilder.CreateTable(
                name: "Excercises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BodyPartId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrainerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TrainerId1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Excercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Excercises_AspNetUsers_TrainerId1",
                        column: x => x.TrainerId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Excercises_BodyParts_BodyPartId",
                        column: x => x.BodyPartId,
                        principalTable: "BodyParts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Excercises_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Excercises_BodyPartId",
                table: "Excercises",
                column: "BodyPartId");

            migrationBuilder.CreateIndex(
                name: "IX_Excercises_CategoryId",
                table: "Excercises",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Excercises_IsDeleted",
                table: "Excercises",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Excercises_TrainerId1",
                table: "Excercises",
                column: "TrainerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Image_Excercises_ExcerciseId",
                table: "Image",
                column: "ExcerciseId",
                principalTable: "Excercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
