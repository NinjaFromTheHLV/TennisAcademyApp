using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;
using TennisAcademyApp.Data.Models;

namespace TennisAcademyApp.Data
{
    public class TennisAcademyDbContext : IdentityDbContext<IdentityUser>
    {
        public const string FacultyNumber = "22180021";

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
        public virtual DbSet<Bag> Bags { get; set; } = null!;
        public virtual DbSet<BagCart> BagCart { get; set; } = null!;
        public virtual DbSet<AuditLog> AuditLogs { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder config)
        {
            base.OnModelCreating(config);

            config.HasDefaultSchema(FacultyNumber);

            config.Entity<Coach>().ToTable(tb => tb.HasTrigger("tr_Coaches_Audit"));
            config.Entity<Reservation>().ToTable(tb => tb.HasTrigger("tr_Reservations_Audit"));

            string auditColumnName = $"LastModified_{FacultyNumber}";

            foreach (var entityType in config.Model.GetEntityTypes())
            {
                if (entityType.ClrType.Namespace != null &&
                    entityType.ClrType.Namespace.StartsWith("Microsoft.AspNetCore.Identity"))
                {
                    continue;
                }

                config.Entity(entityType.ClrType)
                    .Property<DateTime>(auditColumnName)
                    .HasDefaultValueSql("GETDATE()")
                    .IsConcurrencyToken();
            }

            config.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            config.Entity<AuditLog>().ToTable("log_22180021");
        }
    }
}