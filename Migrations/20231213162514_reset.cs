using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedbaseApi.Migrations
{
    /// <inheritdoc />
    public partial class reset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "Articles",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Summary = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Writer = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        DatePosted = table.Column<DateTime>(type: "datetime2", nullable: false),
            //        ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Articles", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Corrections",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        QuestionId = table.Column<int>(type: "int", nullable: false),
            //        QuestionChild = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        SuggestedAnswer = table.Column<bool>(type: "bit", nullable: false),
            //        SuggestedExplanation = table.Column<string>(type: "nvarchar(max)", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Corrections", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Courses",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        CourseRef = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Courses", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Questions",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        QuestionMain = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        ChildA = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        ChildB = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        ChildC = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        ChildD = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        ChildE = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        AnswerA = table.Column<bool>(type: "bit", nullable: false),
            //        AnswerB = table.Column<bool>(type: "bit", nullable: false),
            //        AnswerC = table.Column<bool>(type: "bit", nullable: false),
            //        AnswerD = table.Column<bool>(type: "bit", nullable: false),
            //        AnswerE = table.Column<bool>(type: "bit", nullable: false),
            //        ExplanationA = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        ExplanationB = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        ExplanationC = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        ExplanationD = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        ExplanationE = table.Column<string>(type: "nvarchar(max)", nullable: true),
            //        TopicRef = table.Column<int>(type: "int", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Questions", x => x.Id);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Topics",
            //    columns: table => new
            //    {
            //        Id = table.Column<long>(type: "bigint", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        CourseRef = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        TopicRef = table.Column<int>(type: "int", nullable: false),
            //        Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Topics", x => x.Id);
            //    });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "Corrections");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Topics");
        }
    }
}
