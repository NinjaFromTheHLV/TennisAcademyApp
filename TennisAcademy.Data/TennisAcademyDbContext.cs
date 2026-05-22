using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using TennisAcademyApp.Data.Models;

namespace TennisAcademyApp.Data
{
    public class TennisAcademyDbContext : IdentityDbContext<ApplicationUser>
    {
        public const string FacultyNumber = "22180021";
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TennisAcademyDbContext(DbContextOptions<TennisAcademyDbContext> options)
            : base(options)
        {
        }

        public TennisAcademyDbContext(
            DbContextOptions<TennisAcademyDbContext> options,
            IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
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
        public DbSet<TournamentCategory> TournamentCategories { get; set; } = null!;
        public DbSet<Tournament> Tournaments { get; set; } = null!;
        public DbSet<TournamentUser> TournamentsUsers { get; set; } = null!;

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

                if (entityType.ClrType == typeof(ApplicationUser))
                {
                    continue;
                }

                config.Entity(entityType.ClrType)
                    .Property<DateTime>(auditColumnName)
                    .HasDefaultValueSql("GETDATE()")
                    .ValueGeneratedOnAddOrUpdate();
            }

            config.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            config.Entity<AuditLog>().ToTable("log_22180021");
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateAuditFields();
            return await base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            UpdateAuditFields();
            return base.SaveChanges();
        }
        private void UpdateAuditFields()
        {
            string auditColumnName = $"LastModified_{FacultyNumber}";

            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Metadata.ClrType.Namespace != null &&
                    entry.Metadata.ClrType.Namespace.StartsWith("Microsoft.AspNetCore.Identity"))
                {
                    continue;
                }

                if (entry.Entity is AuditLog || entry.Entity is ApplicationUser)
                {
                    continue;
                }

                if (entry.State == EntityState.Added || entry.State == EntityState.Modified)
                {
                    entry.Property(auditColumnName).CurrentValue = DateTime.UtcNow;
                }
            }
        }
    }
}