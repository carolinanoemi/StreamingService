using Microsoft.EntityFrameworkCore;
using RatingService.Domain.Entities;

namespace RatingService.Infrastructure.Data
{
    public partial class RatingDbContext : DbContext
    {
        public RatingDbContext()
        {
        }

        public RatingDbContext(DbContextOptions<RatingDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Rating> Ratings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rating>(entity =>
            {
                // InitialCreate (Schema)
                entity.ToTable("Ratings", "dbo");

                entity.HasKey(e => e.RatingId);

                entity.Property(e => e.Comment)
                      .HasMaxLength(500);

                entity.Property(e => e.CreatedAt)
                      .HasColumnType("datetime2");
                
                //AddConstraints

                // Check Constraint 
                entity.ToTable(t => t.HasCheckConstraint("CK_Ratings_Score", "Score BETWEEN 1 AND 5"));

                // Unique Compostie Index (En bruger må kun rate en video én gang)
                entity.HasIndex(e => new { e.UserId, e.VideoId }, "UQ_Ratings_User_Video")
                      .IsUnique();

                //  AddIndexes (Performance)

                // Index på VideoId
                entity.HasIndex(e => e.VideoId, "IX_Rating_VideoId");



            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
