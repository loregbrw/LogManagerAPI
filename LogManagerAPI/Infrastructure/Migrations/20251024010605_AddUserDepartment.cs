using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserDepartment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "user_department_id",
                table: "tb_user",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserDepartment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDepartment", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_user_user_department_id",
                table: "tb_user",
                column: "user_department_id");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_user_UserDepartment_user_department_id",
                table: "tb_user",
                column: "user_department_id",
                principalTable: "UserDepartment",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_user_UserDepartment_user_department_id",
                table: "tb_user");

            migrationBuilder.DropTable(
                name: "UserDepartment");

            migrationBuilder.DropIndex(
                name: "IX_tb_user_user_department_id",
                table: "tb_user");

            migrationBuilder.DropColumn(
                name: "user_department_id",
                table: "tb_user");
        }
    }
}
