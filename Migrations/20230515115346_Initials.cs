using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Valkyrie.Entities;

#nullable disable

namespace Valkyrie.Migrations
{
    /// <inheritdoc />
    public partial class Initials : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DevBoards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Guid = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Platform = table.Column<string>(type: "text", nullable: false),
                    Family = table.Column<string>(type: "text", nullable: false),
                    MainMCU = table.Column<MCU>(type: "jsonb", nullable: false),
                    Pins = table.Column<Pins>(type: "jsonb", nullable: false),
                    Communications = table.Column<Communications>(type: "jsonb", nullable: false),
                    Power = table.Column<Power>(type: "jsonb", nullable: false),
                    Dimensions = table.Column<Dimensions>(type: "jsonb", nullable: false),
                    JTAG = table.Column<JTAG>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DevBoards", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DevBoards_Guid",
                table: "DevBoards",
                column: "Guid",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DevBoards");
        }
    }
}
