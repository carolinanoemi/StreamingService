using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WatchHistoryService.Domain.Entities;

namespace WatchHistoryService.Infrastructure.Data;

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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WatchEvent>(entity =>
        {
            entity.HasKey(e => e.WatchEventId).HasName("PK__WatchEve__E2ED3E155FD84368");

            entity.HasIndex(e => e.UserId, "IX_WatchEvents_UserId");

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
