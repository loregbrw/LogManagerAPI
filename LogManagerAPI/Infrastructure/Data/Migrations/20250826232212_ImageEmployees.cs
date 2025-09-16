using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ImageEmployees : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_image",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    file_guid = table.Column<Guid>(type: "uuid", nullable: false),
                    image_content_small = table.Column<byte[]>(type: "bytea", nullable: false),
                    image_content_medium = table.Column<byte[]>(type: "bytea", nullable: false),
                    image_content_large = table.Column<byte[]>(type: "bytea", nullable: false),
                    extension = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.id);
                    table.UniqueConstraint("AK_tb_image_file_guid", x => x.file_guid);
                });

            migrationBuilder.CreateTable(
                name: "tb_employee",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<short>(type: "smallint", nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    password = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    role = table.Column<short>(type: "smallint", nullable: false),
                    profile_image_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.id);
                    table.ForeignKey(
                        name: "FK_tb_employee_tb_image_profile_image_id",
                        column: x => x.profile_image_id,
                        principalTable: "tb_image",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_employee_profile_image_id",
                table: "tb_employee",
                column: "profile_image_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_employee");

            migrationBuilder.DropTable(
                name: "tb_image");
        }
    }
}
