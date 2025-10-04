using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateStock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_stock_tb_stock_department_stock_department_id",
                table: "tb_stock");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_stock_tb_stock_subgroup_stock_subgroup_id",
                table: "tb_stock");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_stock_tb_unit_of_measurement_unit_of_measurement_id",
                table: "tb_stock");

            migrationBuilder.AlterColumn<Guid>(
                name: "unit_of_measurement_id",
                table: "tb_stock",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "stock_subgroup_id",
                table: "tb_stock",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<short>(
                name: "stock_group",
                table: "tb_stock",
                type: "smallint",
                nullable: true,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AlterColumn<Guid>(
                name: "stock_department_id",
                table: "tb_stock",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_stock_tb_stock_department_stock_department_id",
                table: "tb_stock",
                column: "stock_department_id",
                principalTable: "tb_stock_department",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_stock_tb_stock_subgroup_stock_subgroup_id",
                table: "tb_stock",
                column: "stock_subgroup_id",
                principalTable: "tb_stock_subgroup",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_stock_tb_unit_of_measurement_unit_of_measurement_id",
                table: "tb_stock",
                column: "unit_of_measurement_id",
                principalTable: "tb_unit_of_measurement",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_stock_tb_stock_department_stock_department_id",
                table: "tb_stock");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_stock_tb_stock_subgroup_stock_subgroup_id",
                table: "tb_stock");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_stock_tb_unit_of_measurement_unit_of_measurement_id",
                table: "tb_stock");

            migrationBuilder.AlterColumn<Guid>(
                name: "unit_of_measurement_id",
                table: "tb_stock",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "stock_subgroup_id",
                table: "tb_stock",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<short>(
                name: "stock_group",
                table: "tb_stock",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0,
                oldClrType: typeof(short),
                oldType: "smallint",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "stock_department_id",
                table: "tb_stock",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_stock_tb_stock_department_stock_department_id",
                table: "tb_stock",
                column: "stock_department_id",
                principalTable: "tb_stock_department",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_stock_tb_stock_subgroup_stock_subgroup_id",
                table: "tb_stock",
                column: "stock_subgroup_id",
                principalTable: "tb_stock_subgroup",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_stock_tb_unit_of_measurement_unit_of_measurement_id",
                table: "tb_stock",
                column: "unit_of_measurement_id",
                principalTable: "tb_unit_of_measurement",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
