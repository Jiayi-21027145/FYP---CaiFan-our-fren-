using System;
using System.Collections.Generic;
using FYP.Models;
using Microsoft.EntityFrameworkCore;

namespace FYP.Services;

public partial class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<FoodPrices> FoodPrices { get; set; }

    public virtual DbSet<FoodTypes> FoodTypes { get; set; }

    public virtual DbSet<ImageUploads> ImageUploads { get; set; }

    public virtual DbSet<NutritionalValues> NutritionalValues { get; set; }

    public virtual DbSet<Reviews> Reviews { get; set; }

    public virtual DbSet<Users> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FoodPrices>(entity =>
        {
            entity.HasKey(e => e.FoodPriceId).HasName("PK__FoodPric__F79F4F7C4A5FAF56");

            entity.Property(e => e.FoodPriceId).HasColumnName("FoodPriceID");
            entity.Property(e => e.FoodTypeId).HasColumnName("FoodTypeID");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.FoodType).WithMany(p => p.FoodPrices)
                .HasForeignKey(d => d.FoodTypeId)
                .HasConstraintName("FK__FoodPrice__FoodT__2B3F6F97");
        });

        modelBuilder.Entity<FoodTypes>(entity =>
        {
            entity.HasKey(e => e.FoodTypeId).HasName("PK__FoodType__D3D1546C09C08368");

            entity.Property(e => e.FoodTypeId).HasColumnName("FoodTypeID");
            entity.Property(e => e.FoodTypeName).HasMaxLength(50);
        });

        modelBuilder.Entity<ImageUploads>(entity =>
        {
            entity.HasKey(e => e.ImageId).HasName("PK__ImageUpl__7516F4EC5D659476");

            entity.Property(e => e.ImageId).HasColumnName("ImageID");
            entity.Property(e => e.FoodTypeId).HasColumnName("FoodTypeID");
            entity.Property(e => e.ImageName).HasMaxLength(255);
            entity.Property(e => e.UploadDate).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.FoodType).WithMany(p => p.ImageUploads)
                .HasForeignKey(d => d.FoodTypeId)
                .HasConstraintName("FK__ImageUplo__FoodT__2F10007B");

            entity.HasOne(d => d.User).WithMany(p => p.ImageUploads)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__ImageUplo__UserI__2E1BDC42");
        });

        modelBuilder.Entity<NutritionalValues>(entity =>
        {
            entity.HasKey(e => e.NutritionalValueId).HasName("PK__Nutritio__F26DDF0E46146EC1");

            entity.Property(e => e.NutritionalValueId).HasColumnName("NutritionalValueID");
            entity.Property(e => e.FoodTypeId).HasColumnName("FoodTypeID");
            entity.Property(e => e.NutrientName).HasMaxLength(50);
            entity.Property(e => e.Value).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.FoodType).WithMany(p => p.NutritionalValues)
                .HasForeignKey(d => d.FoodTypeId)
                .HasConstraintName("FK__Nutrition__FoodT__286302EC");
        });

        modelBuilder.Entity<Reviews>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__Reviews__74BC79AEDDE7E710");

            entity.Property(e => e.ReviewId).HasColumnName("ReviewID");
            entity.Property(e => e.FoodTypeId).HasColumnName("FoodTypeID");
            entity.Property(e => e.ReviewDate).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.FoodType).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.FoodTypeId)
                .HasConstraintName("FK__Reviews__FoodTyp__32E0915F");

            entity.HasOne(d => d.User).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Reviews__UserID__31EC6D26");
        });

        modelBuilder.Entity<Users>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC55FFB037");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
