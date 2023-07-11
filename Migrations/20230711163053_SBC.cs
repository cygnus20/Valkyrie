using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Valkyrie.Entities;

#nullable disable

namespace Valkyrie.Migrations
{
    /// <inheritdoc />
    public partial class SBC : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SBC",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Guid = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Platform = table.Column<string>(type: "text", nullable: false),
                    OperatingSystems = table.Column<List<string>>(type: "jsonb", nullable: false),
                    Sensors = table.Column<List<string>>(type: "jsonb", nullable: false),
                    ExtraInterfaces = table.Column<List<string>>(type: "jsonb", nullable: true),
                    Power = table.Column<Power>(type: "jsonb", nullable: false),
                    Pins = table.Column<Pins>(type: "jsonb", nullable: false),
                    Communications = table.Column<Communications>(type: "jsonb", nullable: false),
                    IO = table.Column<List<string>>(type: "jsonb", nullable: false),
                    NetworkingAndComm = table.Column<List<NetworkComm>>(type: "jsonb", nullable: false),
                    SpecialFeatures = table.Column<List<string>>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SBC", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SBC_Guid",
                table: "SBC",
                column: "Guid",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SBC");
        }
    }
}
