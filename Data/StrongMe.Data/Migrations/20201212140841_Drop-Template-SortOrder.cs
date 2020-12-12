namespace StrongMe.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class DropTemplateSortOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SortOrder",
                table: "TemplateProgramDetails");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SortOrder",
                table: "TemplateProgramDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
