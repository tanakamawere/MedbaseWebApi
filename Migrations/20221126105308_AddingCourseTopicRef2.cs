using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedbaseApi.Migrations
{
    /// <inheritdoc />
    public partial class AddingCourseTopicRef2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CourseRef",
                table: "Topics",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseRef",
                table: "Topics");
        }
    }
}
