using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BusinessCard.Core.Data;

public partial class ProgressSoftContext : DbContext
{
    public ProgressSoftContext()
    {
    }

    public ProgressSoftContext(DbContextOptions<ProgressSoftContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BusinessCard> BusinessCards { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-63EPU7C\\SQL2024;Database=ProgressSoft;User Id=sa;Password=1234;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BusinessCard>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Business__3214EC07B7D2383C");

            entity.ToTable("BusinessCard");

            entity.HasIndex(e => e.Phone, "UQ__Business__5C7E359E1B73BA61").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Business__A9D1053425126977").IsUnique();

            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
