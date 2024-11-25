using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ILIS.Newton.Assignment.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VideoGames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Developer = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoGames", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "VideoGames",
                columns: new[] { "Id", "Developer", "Genre", "Price", "ReleaseDate", "Title" },
                values: new object[,]
                {
                    { 1, "Nintendo", "Adventure", 59.99m, new DateTime(1986, 2, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Legend of Zelda" },
                    { 2, "CD Projekt Red", "RPG", 49.99m, new DateTime(2020, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cyberpunk 2077" },
                    { 3, "GSC Game World", "Survival", 69.99m, new DateTime(2024, 4, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Stalker 2: Heart of Chornobyl" },
                    { 4, "CD Projekt Red", "RPG", 39.99m, new DateTime(2015, 5, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Witcher 3: Wild Hunt" },
                    { 5, "Sucker Punch Productions", "Action", 59.99m, new DateTime(2020, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ghost of Tsushima" },
                    { 6, "Bethesda Game Studios", "RPG", 29.99m, new DateTime(2015, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fallout 4" },
                    { 7, "FromSoftware", "RPG", 59.99m, new DateTime(2022, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Elden Ring" },
                    { 8, "Guerrilla Games", "Action", 49.99m, new DateTime(2017, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Horizon Zero Dawn" },
                    { 9, "Rockstar Games", "Adventure", 59.99m, new DateTime(2018, 10, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "Red Dead Redemption 2" },
                    { 10, "Santa Monica Studio", "Action", 49.99m, new DateTime(2018, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "God of War" },
                    { 11, "Ubisoft", "RPG", 59.99m, new DateTime(2020, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Assassin's Creed Valhalla" },
                    { 12, "BioWare", "RPG", 59.99m, new DateTime(2021, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mass Effect: Legendary Edition" },
                    { 13, "FromSoftware", "RPG", 39.99m, new DateTime(2016, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dark Souls III" },
                    { 14, "Naughty Dog", "Action", 59.99m, new DateTime(2020, 6, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Last of Us Part II" },
                    { 15, "Blizzard Entertainment", "RPG", 69.99m, new DateTime(2023, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Diablo IV" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VideoGames");
        }
    }
}
