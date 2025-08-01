using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TennisAcademyApp.Data.Models;

namespace TennisAcademyApp.Data.Seeding
{
    public class TrainingTypeSeeding : IEntityTypeConfiguration<TrainingType>
    {
        public void Configure(EntityTypeBuilder<TrainingType> config)
        {
            config.HasData(
                new TrainingType
                {
                    Id = 1,
                    Name = "Physical Conditioning Routine"
                },
                new TrainingType
                {
                    Id = 2,
                    Name = "Technical Skill Development"
                },
                new TrainingType
                {
                    Id = 3,
                    Name = "Tactical Game Strategy"
                },
                new TrainingType
                {
                    Id = 4,
                    Name = "Mental Toughness Training"
                }
            );
        }
    }
}
