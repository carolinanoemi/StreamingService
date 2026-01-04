using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SubscriptionService.Domain.Entities;

namespace SubscriptionService.Infrastructure.Data;

public partial class WatchHistoryDbContext : DbContext
{
    public WatchHistoryDbContext()
    {
    }

    public WatchHistoryDbContext(DbContextOptions<WatchHistoryDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<WatchEvent> WatchEvents { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=127.0.0.1,1433;Database=StreamingService_WatchHistory;User Id=sa;Password=Your_password123!;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WatchEvent>(entity =>
        {
            entity.HasKey(e => e.WatchEventId).HasName("PK__WatchEve__E2ED3E155FD84368");

            entity.HasIndex(e => new { e.UserId, e.WatchedAt }, "IX_WatchEvents_UserId_WatchedAt").IsDescending(false, true);

            entity.HasIndex(e => e.WatchedAt, "IX_WatchEvents_WatchedAt").IsDescending();

            entity.Property(e => e.DeviceType)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
