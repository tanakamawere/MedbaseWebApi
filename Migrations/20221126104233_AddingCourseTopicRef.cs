using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedbaseApi.Migrations
{
    /// <inheritdoc />
    public partial class AddingCourseTopicRef : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TopicRef",
                table: "Topics",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CourseRef",
                table: "Courses",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TopicRef",
                table: "Topics");

            migrationBuilder.DropColumn(
                name: "CourseRef",
                table: "Courses");
        }
    }
}
