using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TennisAcademyApp.Data.Models;

namespace TennisAcademyApp.Data.Configurations
{
    public class RacketCartConfiguration : IEntityTypeConfiguration<RacketCart>
    {
        public void Configure(EntityTypeBuilder<RacketCart> config)
        {
            config
                .HasOne(rc => rc.Racket)
                .WithMany(r => r.RacketCart)
                .HasForeignKey(rc => rc.RacketId);

            config
                .HasOne(rc => rc.User)
                .WithMany()
                .HasForeignKey(rc => rc.UserId);
        }
    }
}
