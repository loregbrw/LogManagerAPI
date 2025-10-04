using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRegister : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_stock");

            migrationBuilder.CreateTable(
                name: "tb_stock_item",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    unit_of_measurement_id = table.Column<Guid>(type: "uuid", nullable: true),
                    localization = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    stock_department_id = table.Column<Guid>(type: "uuid", nullable: true),
                    stock_group = table.Column<short>(type: "smallint", nullable: true),
                    stock_subgroup_id = table.Column<Guid>(type: "uuid", nullable: true),
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
                    table.PrimaryKey("PK_StockItem", x => x.id);
                    table.ForeignKey(
                        name: "FK_tb_stock_item_tb_stock_department_stock_department_id",
                        column: x => x.stock_department_id,
                        principalTable: "tb_stock_department",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_tb_stock_item_tb_stock_subgroup_stock_subgroup_id",
                        column: x => x.stock_subgroup_id,
                        principalTable: "tb_stock_subgroup",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_tb_stock_item_tb_unit_of_measurement_unit_of_measurement_id",
                        column: x => x.unit_of_measurement_id,
                        principalTable: "tb_unit_of_measurement",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "tb_register",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    register_type = table.Column<short>(type: "smallint", nullable: false),
                    stock_item_id = table.Column<Guid>(type: "uuid", nullable: false),
                    amount = table.Column<double>(type: "double precision", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    observation = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Register", x => x.id);
                    table.ForeignKey(
                        name: "FK_tb_register_tb_stock_item_stock_item_id",
                        column: x => x.stock_item_id,
                        principalTable: "tb_stock_item",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_register_tb_user_user_id",
                        column: x => x.user_id,
                        principalTable: "tb_user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_register_stock_item_id",
                table: "tb_register",
                column: "stock_item_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_register_user_id",
                table: "tb_register",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_stock_item_stock_department_id",
                table: "tb_stock_item",
                column: "stock_department_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_stock_item_stock_subgroup_id",
                table: "tb_stock_item",
                column: "stock_subgroup_id");

            migrationBuilder.CreateIndex(
                name: "IX_tb_stock_item_unit_of_measurement_id",
                table: "tb_stock_item",
                column: "unit_of_measurement_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_register");

            migrationBuilder.DropTable(
                name: "tb_stock_item");

            migrationBuilder.CreateTable(
                name: "tb_stock",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    stock_department_id = table.Column<Guid>(type: "uuid", nullable: true),
                    stock_subgroup_id = table.Column<Guid>(type: "uuid", nullable: true),
                    unit_of_measurement_id = table.Column<Guid>(type: "uuid", nullable: true),
                    code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    cost = table.Column<decimal>(type: "numeric", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    current = table.Column<int>(type: "integer", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    inbound = table.Column<int>(type: "integer", nullable: false),
                    localization = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    minimum_stock = table.Column<short>(type: "smallint", nullable: true),
                    outbound = table.Column<int>(type: "integer", nullable: false),
                    stock_group = table.Column<short>(type: "smallint", nullable: true),
                    stock_situation = table.Column<short>(type: "smallint", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stock", x => x.id);
                    table.ForeignKey(
                        name: "FK_tb_stock_tb_stock_department_stock_department_id",
                        column: x => x.stock_department_id,
                        principalTable: "tb_stock_department",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_tb_stock_tb_stock_subgroup_stock_subgroup_id",
                        column: x => x.stock_subgroup_id,
                        principalTable: "tb_stock_subgroup",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_tb_stock_tb_unit_of_measurement_unit_of_measurement_id",
                        column: x => x.unit_of_measurement_id,
                        principalTable: "tb_unit_of_measurement",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
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
    }
}
