using ILIS.Newton.Assignment.Entities.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ILIS.Newton.Assignment.DataAccess.Configuration
{
    public class VideoGameConfiguration : IEntityTypeConfiguration<VideoGame>
    {
        public void Configure(EntityTypeBuilder<VideoGame> builder)
        {
            builder.HasKey(v => v.Id);

            builder.Property(v => v.Title)
                .IsRequired()
                .HasMaxLength(100); 

            builder.Property(v => v.Genre)
                .HasMaxLength(50); 

            builder.Property(v => v.Developer)
                .HasMaxLength(100); 

            builder.Property(v => v.Price)
                .HasColumnType("decimal(18,2)")
            .IsRequired();

            builder.HasData(
                new VideoGame { Id = 1, Title = "The Legend of Zelda", Genre = "Adventure", Developer = "Nintendo", ReleaseDate = new DateTime(1986, 2, 21), Price = 59.99m },
                new VideoGame { Id = 2, Title = "Cyberpunk 2077", Genre = "RPG", Developer = "CD Projekt Red", ReleaseDate = new DateTime(2020, 12, 10), Price = 49.99m },
                new VideoGame { Id = 3, Title = "Stalker 2: Heart of Chornobyl", Genre = "Survival", Developer = "GSC Game World", ReleaseDate = new DateTime(2024, 4, 28), Price = 69.99m },
                new VideoGame { Id = 4, Title = "The Witcher 3: Wild Hunt", Genre = "RPG", Developer = "CD Projekt Red", ReleaseDate = new DateTime(2015, 5, 19), Price = 39.99m },
                new VideoGame { Id = 5, Title = "Ghost of Tsushima", Genre = "Action", Developer = "Sucker Punch Productions", ReleaseDate = new DateTime(2020, 7, 17), Price = 59.99m },
                new VideoGame { Id = 6, Title = "Fallout 4", Genre = "RPG", Developer = "Bethesda Game Studios", ReleaseDate = new DateTime(2015, 11, 10), Price = 29.99m },
                new VideoGame { Id = 7, Title = "Elden Ring", Genre = "RPG", Developer = "FromSoftware", ReleaseDate = new DateTime(2022, 2, 25), Price = 59.99m },
                new VideoGame { Id = 8, Title = "Horizon Zero Dawn", Genre = "Action", Developer = "Guerrilla Games", ReleaseDate = new DateTime(2017, 2, 28), Price = 49.99m },
                new VideoGame { Id = 9, Title = "Red Dead Redemption 2", Genre = "Adventure", Developer = "Rockstar Games", ReleaseDate = new DateTime(2018, 10, 26), Price = 59.99m },
                new VideoGame { Id = 10, Title = "God of War", Genre = "Action", Developer = "Santa Monica Studio", ReleaseDate = new DateTime(2018, 4, 20), Price = 49.99m },
                new VideoGame { Id = 11, Title = "Assassin's Creed Valhalla", Genre = "RPG", Developer = "Ubisoft", ReleaseDate = new DateTime(2020, 11, 10), Price = 59.99m },
                new VideoGame { Id = 12, Title = "Mass Effect: Legendary Edition", Genre = "RPG", Developer = "BioWare", ReleaseDate = new DateTime(2021, 5, 14), Price = 59.99m },
                new VideoGame { Id = 13, Title = "Dark Souls III", Genre = "RPG", Developer = "FromSoftware", ReleaseDate = new DateTime(2016, 4, 12), Price = 39.99m },
                new VideoGame { Id = 14, Title = "The Last of Us Part II", Genre = "Action", Developer = "Naughty Dog", ReleaseDate = new DateTime(2020, 6, 19), Price = 59.99m },
                new VideoGame { Id = 15, Title = "Diablo IV", Genre = "RPG", Developer = "Blizzard Entertainment", ReleaseDate = new DateTime(2023, 6, 6), Price = 69.99m }
            );
        }
    }
}