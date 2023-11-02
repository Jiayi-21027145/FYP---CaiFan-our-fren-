using System;
using System.Collections.Generic;
using FYP2.Models;
using Microsoft.EntityFrameworkCore;

namespace FYP2.Services;

public partial class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Dataset> Dataset { get; set; }

    public virtual DbSet<ImageUploads> ImageUploads { get; set; }

    public virtual DbSet<Menu> Menu { get; set; }

    public virtual DbSet<Reviews> Reviews { get; set; }

    public virtual DbSet<UserRole> UserRole { get; set; }

    public virtual DbSet<UserRoles> UserRoles { get; set; }

    public virtual DbSet<Users> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Dataset>(entity =>
        {
            entity.HasKey(e => e.ImageId).HasName("PK__Dataset__7516F4EC2C688E76");

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
            entity.HasKey(e => e.UploadId).HasName("PK__ImageUpl__6D16C86DC9A2FB0A");

            entity.Property(e => e.UploadId).HasColumnName("UploadID");
            entity.Property(e => e.ImageName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("ImageURL");
            entity.Property(e => e.UploadDate).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.ImageUploads)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_ImageUploads_Users");
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.FoodName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FoodType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NutrientName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.SellingPrice).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<Reviews>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__Reviews__74BC79AEA0877919");

            entity.Property(e => e.ReviewId).HasColumnName("ReviewID");
            entity.Property(e => e.Comment).IsUnicode(false);
            entity.Property(e => e.ImageId).HasColumnName("ImageID");
            entity.Property(e => e.ReviewDate).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Image).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.ImageId)
                .HasConstraintName("FK_Reviews_Dataset");

            entity.HasOne(d => d.User).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Reviews_Users");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__UserRole__8AFACE3A7A2B652E");

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UserRoles>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserRole__3214EC2776F9990A");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_UserRoles_Roles");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserRoles_Users");
        });

        modelBuilder.Entity<Users>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC8B970562");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Reference)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
