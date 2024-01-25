using System;
using System.Collections.Generic;
using Lesson05.Models;
using Microsoft.EntityFrameworkCore;

namespace Lesson05.Services;

public partial class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BoundingBox> BoundingBox { get; set; }

    public virtual DbSet<Dataset> Dataset { get; set; }

    public virtual DbSet<Dish> Dish { get; set; }

    public virtual DbSet<Food> Food { get; set; }

    public virtual DbSet<ImageUploads> ImageUploads { get; set; }

    public virtual DbSet<JiakUser> JiakUser { get; set; }

    public virtual DbSet<Menu> Menu { get; set; }

    public virtual DbSet<Prediction> Prediction { get; set; }

    public virtual DbSet<Reviews> Reviews { get; set; }

    public virtual DbSet<Summary> Summary { get; set; }

    public virtual DbSet<UserHistory> UserHistory { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BoundingBox>(entity =>
        {
            entity.HasKey(e => e.BoxId).HasName("PK__Bounding__136CF76454160AFA");

            entity.HasOne(d => d.Prediction).WithMany(p => p.BoundingBox)
                .HasForeignKey(d => d.PredictionId)
                .HasConstraintName("FK__BoundingB__Predi__5C02A283");
        });

        modelBuilder.Entity<Dataset>(entity =>
        {
            entity.HasKey(e => e.ImageId).HasName("PK__Dataset__7516F70C82494E8A");

            entity.Property(e => e.DateTime).HasColumnType("datetime");
            entity.Property(e => e.DishName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ImageName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Location)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Dish>(entity =>
        {
            entity.HasKey(e => e.FoodId).HasName("PK__Dish__856DB3EB7A34B0B7");

            entity.Property(e => e.HighestNv).HasColumnName("HighestNV");
            entity.Property(e => e.LowestNv).HasColumnName("LowestNV");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Food>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.FoodFive)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FoodFour)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FoodOne)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FoodSix)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FoodThree)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FoodTwo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UserId)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ImageUploads>(entity =>
        {
            entity.HasKey(e => e.UploadId).HasName("PK__ImageUpl__6D16C86D927ADC7A");

            entity.Property(e => e.UploadId).HasColumnName("UploadID");
            entity.Property(e => e.ImageData).IsUnicode(false);
            entity.Property(e => e.ImageDt).HasColumnType("datetime");
            entity.Property(e => e.ImageLc)
                .HasMaxLength(100)
                .IsUnicode(false);
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
            entity.HasKey(e => e.UserId).HasName("PK__JiakUser__1788CC4C858C8263");

            entity.Property(e => e.UserId)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.LastLogin).HasColumnType("datetime");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserPw).HasMaxLength(50);
            entity.Property(e => e.UserRole)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.FoodName).HasName("PK__Menu__81B4FC24A06B1F16");

            entity.Property(e => e.FoodName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.AveragePrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.HighestPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ImageData).HasColumnType("text");
            entity.Property(e => e.LowestPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.NutrientName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Prediction>(entity =>
        {
            entity.HasKey(e => e.PredictionId).HasName("PK__Predicti__BAE4C1A09C7D1749");

            entity.Property(e => e.HighestPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.LowestPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.MenuId)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.TagName)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Dataset).WithMany(p => p.Prediction)
                .HasForeignKey(d => d.DatasetId)
                .HasConstraintName("FK__Predictio__Datas__592635D8");

            entity.HasOne(d => d.Menu).WithMany(p => p.Prediction)
                .HasForeignKey(d => d.MenuId)
                .HasConstraintName("FK__Predictio__MenuI__5832119F");
        });

        modelBuilder.Entity<Reviews>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__Reviews__74BC79AE339AE35B");

            entity.Property(e => e.ReviewId).HasColumnName("ReviewID");
            entity.Property(e => e.Comment).IsUnicode(false);
            entity.Property(e => e.ImageData).IsUnicode(false);
            entity.Property(e => e.PublishDate).HasColumnType("date");
        });

        modelBuilder.Entity<Summary>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.UserId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("UserID");
        });

        modelBuilder.Entity<UserHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserHist__3214EC07EF45DF15");

            entity.Property(e => e.BatangFish)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BoiledEgg)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BraisedMeat)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BrownRice)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CrispyMeat)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CrispyMeatWsauce)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CrispyMeatWSauce");
            entity.Property(e => e.Image).IsUnicode(false);
            entity.Property(e => e.Leafy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NonLeafy)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Omellete)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SteamedEgg)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserId)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.WhiteFish)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.WhiteRice)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.UserHistory)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserHistory_JiakUser");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
