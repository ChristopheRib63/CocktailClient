using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CocktailClient.Server.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alcools",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alcools", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SuperRecettes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Ingredient = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    AlcoolId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuperRecettes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SuperRecettes_Alcools_AlcoolId",
                        column: x => x.AlcoolId,
                        principalTable: "Alcools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Alcools",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Avec Alcool" },
                    { 2, "Sans Alcool" }
                });

            migrationBuilder.InsertData(
                table: "SuperRecettes",
                columns: new[] { "Id", "AlcoolId", "Description", "Ingredient", "Name" },
                values: new object[,]
                {
                    { 1, 1, "boisson forte", "Rhum", "ABC" },
                    { 2, 2, "à consommer à tout heure", "eau, sirop", "Grenadine" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_SuperRecettes_AlcoolId",
                table: "SuperRecettes",
                column: "AlcoolId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SuperRecettes");

            migrationBuilder.DropTable(
                name: "Alcools");
        }
    }
}
