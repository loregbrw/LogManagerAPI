using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_stock_department",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockDepartment", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_stock_subgroup",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockSubgroup", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_unit_of_measurement",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitOfMeasurement", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_stock",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    unit_of_measurement_id = table.Column<Guid>(type: "uuid", nullable: false),
                    localization = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    stock_department_id = table.Column<Guid>(type: "uuid", nullable: false),
                    stock_group = table.Column<short>(type: "smallint", nullable: false),
                    stock_subgroup_id = table.Column<Guid>(type: "uuid", nullable: false),
                    cost = table.Column<decimal>(type: "numeric", nullable: true),
                    minimum_stock = table.Column<short>(type: "smallint", nullable: true),
                    inbound = table.Column<int>(type: "integer", nullable: false),
                    outbound = table.Column<int>(type: "integer", nullable: false),
                    current = table.Column<int>(type: "integer", nullable: false),
                    stock_situation = table.Column<short>(type: "smallint", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stock", x => x.id);
                    table.ForeignKey(
                        name: "FK_tb_stock_tb_stock_department_stock_department_id",
                        column: x => x.stock_department_id,
                        principalTable: "tb_stock_department",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_stock_tb_stock_subgroup_stock_subgroup_id",
                        column: x => x.stock_subgroup_id,
                        principalTable: "tb_stock_subgroup",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_stock_tb_unit_of_measurement_unit_of_measurement_id",
                        column: x => x.unit_of_measurement_id,
                        principalTable: "tb_unit_of_measurement",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_stock_stock_department_id",
                table: "tb_stock",
                column: "stock_department_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_stock_stock_subgroup_id",
                table: "tb_stock",
                column: "stock_subgroup_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_stock_unit_of_measurement_id",
                table: "tb_stock",
                column: "unit_of_measurement_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_stock");

            migrationBuilder.DropTable(
                name: "tb_stock_department");

            migrationBuilder.DropTable(
                name: "tb_stock_subgroup");

            migrationBuilder.DropTable(
                name: "tb_unit_of_measurement");
        }
    }
}
