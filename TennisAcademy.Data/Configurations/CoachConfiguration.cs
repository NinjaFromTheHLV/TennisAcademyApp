using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TennisAcademyApp.Data.Models;
using static TennisAcademyApp.GCommon.Validations.ValidationConstants.Coach;

namespace TennisAcademyApp.Data.Configurations
{
    public class CoachConfiguration : IEntityTypeConfiguration<Coach>
    {
        public void Configure(EntityTypeBuilder<Coach> config)
        {
            config
                .HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId);

            config
                .Property(c => c.Name)
                .IsUnicode(true)
                .HasMaxLength(CoachNameMaxLenght);

            config
                .Property(c => c.Description)
                .HasMaxLength(CoachDescriptionMaxLenght);

            config
                .HasData(SeededCoaches());
        }
        private List<Coach> SeededCoaches()
        {
            List<Coach> seededCoaches = new List<Coach>()
            {
                new Coach
                {
                    CoachId = 1,
                    Name = "Rafael Nadal",
                    Age = 38,
                    Description = "One of the greatest tennis players of all time, known for his clay court dominance.",
                    Nationality = "Spanish",
                    UserId = "90222459-c5ca-436e-a8b3-b92e0669c683",
                    ImageUrl = "~/pictures/rafa.jpg"
                },
                new Coach
                {
                    CoachId = 2,
                    Name = "Roger Federer",
                    Age = 43,
                    Description = "Swiss tennis legend with unmatched elegance and 20 Grand Slam titles.",
                    Nationality = "Swiss",
                    UserId = "90222459-c5ca-436e-a8b3-b92e0669c683",
                    ImageUrl = "https://a.espncdn.com/combiner/i?img=/i/headshots/tennis/players/full/425.png"
                },
                new Coach
                {
                    CoachId = 3,
                    Name = "Novak Djokovic",
                    Age = 37,
                    Description = "Serbian champion, known for his resilience and complete game.",
                    Nationality = "Serbian",
                    UserId = "90222459-c5ca-436e-a8b3-b92e0669c683",
                    ImageUrl = "https://a.espncdn.com/i/headshots/tennis/players/full/296.png"
                },
                new Coach
                {
                    CoachId = 4,
                    Name = "Andre Agassi",
                    Age = 55,
                    Description = "American icon who redefined tennis in the 90s with a colorful personality.",
                    Nationality = "American",
                    UserId = "90222459-c5ca-436e-a8b3-b92e0669c683",
                    ImageUrl = "https://www.atptour.com/-/media/alias/player-headshot/A092"
                },
                new Coach
                {
                    CoachId = 5,
                    Name = "Björn Borg",
                    Age = 68,
                    Description = "Swedish legend with ice-cold nerves and six French Open titles.",
                    Nationality = "Swedish",
                    UserId = "90222459-c5ca-436e-a8b3-b92e0669c683",
                    ImageUrl = "https://lavercup.com/wp-content/uploads/2022/12/figure-borg-2.png"
                }
            };
            return seededCoaches;
        }
    }
}
