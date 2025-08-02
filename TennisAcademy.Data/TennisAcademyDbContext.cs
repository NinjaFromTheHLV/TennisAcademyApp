using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TennisAcademyApp.Data.Models;

namespace TennisAcademyApp.Data
{
    public class TennisAcademyDbContext : IdentityDbContext
    {
        public TennisAcademyDbContext(DbContextOptions<TennisAcademyDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Coach> Coaches { get; set; } = null!;
        public virtual DbSet<Surface> Surfaces { get; set; } = null!;
        public virtual DbSet<Reservation> Reservations { get; set; } = null!;
        public virtual DbSet<TrainingType> Trainings { get; set; } = null!;
        public virtual DbSet<UserFavourite> UserFavourites { get; set; } = null!;
        public virtual DbSet<Racket> Rackets { get; set; } = null!;
        public virtual DbSet<RacketCart> RacketCart { get; set; } = null!;
        public virtual DbSet<Ball> Balls { get; set; } = null!;
        public virtual DbSet<BallCart> BallCart { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder config)
        {
            base.OnModelCreating(config);

            config.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
