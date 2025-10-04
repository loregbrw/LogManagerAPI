using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRegister : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_register_tb_user_user_id",
                table: "tb_register");

            migrationBuilder.AlterColumn<Guid>(
                name: "user_id",
                table: "tb_register",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_register_tb_user_user_id",
                table: "tb_register",
                column: "user_id",
                principalTable: "tb_user",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_register_tb_user_user_id",
                table: "tb_register");

            migrationBuilder.AlterColumn<Guid>(
                name: "user_id",
                table: "tb_register",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_register_tb_user_user_id",
                table: "tb_register",
                column: "user_id",
                principalTable: "tb_user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
