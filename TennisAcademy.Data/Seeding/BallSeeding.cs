using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TennisAcademyApp.Data.Models;

namespace TennisAcademyApp.Data.Seeding
{
    public class BallSeeding : IEntityTypeConfiguration<Ball>
    {
        public void Configure(EntityTypeBuilder<Ball> config)
        {
            config.HasData(
                new Ball
                {
                    Id = 1,
                    Brand = "Wilson",
                    Model = "US Open Extra Duty",
                    Price = 12.99m,
                    Quantity = 50,
                    ImageUrl = "https://m.media-amazon.com/images/I/715MEN61aPL._UF1000,1000_QL80_.jpg"
                },
                new Ball
                {
                    Id = 2,
                    Brand = "Head",
                    Model = "Tour XT",
                    Price = 11.49m,
                    Quantity = 35,
                    ImageUrl = "https://cdn.sportdepot.bg/files/catalog/detail/570823_01.jpg"
                },
                new Ball
                {
                    Id = 3,
                    Brand = "Dunlop",
                    Model = "ATP Championship",
                    Price = 10.99m,
                    Quantity = 40,
                    ImageUrl = "https://m.media-amazon.com/images/I/618MvroxyXL._UF1000,1000_QL80_.jpg"
                }
            );
        }
    }
}
