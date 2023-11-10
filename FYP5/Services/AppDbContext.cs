using System;
using System.Collections.Generic;
using FYP5.Models;
using Microsoft.EntityFrameworkCore;

namespace FYP5.Services;

public partial class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Dataset> Dataset { get; set; }

    public virtual DbSet<ImageUploads> ImageUploads { get; set; }

    public virtual DbSet<JiakUser> JiakUser { get; set; }

    public virtual DbSet<Menu> Menu { get; set; }

    public virtual DbSet<Reviews> Reviews { get; set; }

    public virtual DbSet<UserHistory> UserHistory { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Dataset>(entity =>
        {
            entity.HasKey(e => e.ImageId).HasName("PK__Dataset__7516F4ECD5EE5907");

            entity.Property(e => e.ImageId).HasColumnName("ImageID");
            entity.Property(e => e.ImageName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("ImageURL");
            entity.Property(e => e.Label)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ImageUploads>(entity =>
        {
            entity.HasKey(e => e.UploadId).HasName("PK__ImageUpl__6D16C86D210EA1B8");

            entity.Property(e => e.UploadId).HasColumnName("UploadID");
            entity.Property(e => e.ImageName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("ImageURL");
            entity.Property(e => e.UploadDate).HasColumnType("datetime");
            entity.Property(e => e.UserId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.ImageUploads)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_ImageUploads_JiakUser");
        });

        modelBuilder.Entity<JiakUser>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__JiakUser__1788CC4C7BBAE37A");

            entity.Property(e => e.UserId)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FullName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("FUllName");
            entity.Property(e => e.LastLogin).HasColumnType("datetime");
            entity.Property(e => e.UserPw).HasMaxLength(50);
            entity.Property(e => e.UserRole)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.AveragePrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.FoodName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FoodType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.HighestPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ImageData)
                .HasMaxLength(70)
                .IsUnicode(false);
            entity.Property(e => e.LowestPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.NutrientName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Reviews>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__Reviews__74BC79AEF0C52B9E");

            entity.Property(e => e.ReviewId).HasColumnName("ReviewID");
            entity.Property(e => e.Comment).IsUnicode(false);
        });

        modelBuilder.Entity<UserHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserHist__3214EC072C20D1B1");

            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
