using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedbaseApi.Migrations
{
    /// <inheritdoc />
    public partial class ChangeSubscriptions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Topics_TopicId",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_SubTiers_SubTierId",
                table: "Subscriptions");

            migrationBuilder.DropTable(
                name: "SubTiers");

            migrationBuilder.DropIndex(
                name: "IX_Subscriptions_SubTierId",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "SubTierId",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Subscriptions");

            migrationBuilder.RenameColumn(
                name: "UserEmail",
                table: "Subscriptions",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "Reference",
                table: "Subscriptions",
                newName: "Email");

            migrationBuilder.AlterColumn<long>(
                name: "TopicId",
                table: "Questions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "AnswerE",
                table: "Questions",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "AnswerD",
                table: "Questions",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "AnswerC",
                table: "Questions",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "AnswerB",
                table: "Questions",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "AnswerA",
                table: "Questions",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Topics_TopicId",
                table: "Questions",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Topics_TopicId",
                table: "Questions");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Subscriptions",
                newName: "UserEmail");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Subscriptions",
                newName: "Reference");

            migrationBuilder.AddColumn<int>(
                name: "SubTierId",
                table: "Subscriptions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Subscriptions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<long>(
                name: "TopicId",
                table: "Questions",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<bool>(
                name: "AnswerE",
                table: "Questions",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "AnswerD",
                table: "Questions",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "AnswerC",
                table: "Questions",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "AnswerB",
                table: "Questions",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "AnswerA",
                table: "Questions",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.CreateTable(
                name: "SubTiers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubTiers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_SubTierId",
                table: "Subscriptions",
                column: "SubTierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Topics_TopicId",
                table: "Questions",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_SubTiers_SubTierId",
                table: "Subscriptions",
                column: "SubTierId",
                principalTable: "SubTiers",
                principalColumn: "Id");
        }
    }
}
