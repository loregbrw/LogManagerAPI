using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserDepartmentUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_user_UserDepartment_user_department_id",
                table: "tb_user");

            migrationBuilder.RenameTable(
                name: "UserDepartment",
                newName: "tb_user_department");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "tb_user_department",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "tb_user_department",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "tb_user_department",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "DeletedAt",
                table: "tb_user_department",
                newName: "deleted_at");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "tb_user_department",
                newName: "created_at");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "tb_user_department",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_user_tb_user_department_user_department_id",
                table: "tb_user",
                column: "user_department_id",
                principalTable: "tb_user_department",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_user_tb_user_department_user_department_id",
                table: "tb_user");

            migrationBuilder.RenameTable(
                name: "tb_user_department",
                newName: "UserDepartment");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "UserDepartment",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "UserDepartment",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "UserDepartment",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "deleted_at",
                table: "UserDepartment",
                newName: "DeletedAt");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "UserDepartment",
                newName: "CreatedAt");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "UserDepartment",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_user_UserDepartment_user_department_id",
                table: "tb_user",
                column: "user_department_id",
                principalTable: "UserDepartment",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
