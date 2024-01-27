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

    public virtual DbSet<Calories> Calories { get; set; }

    public virtual DbSet<Dataset> Dataset { get; set; }

    public virtual DbSet<Dish> Dish { get; set; }

    public virtual DbSet<Food> Food { get; set; }

    public virtual DbSet<History> History { get; set; }

    public virtual DbSet<JiakUser> JiakUser { get; set; }

    public virtual DbSet<Menu> Menu { get; set; }

    public virtual DbSet<Prediction> Prediction { get; set; }

    public virtual DbSet<Reviews> Reviews { get; set; }

    public virtual DbSet<Summary> Summary { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Calories>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Calories__3214EC07E5B71D8E");
        });

        modelBuilder.Entity<Dataset>(entity =>
        {
            entity.HasKey(e => e.ImageId).HasName("PK__Dataset__7516F70CA181AF10");

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
            entity.HasKey(e => e.FoodId).HasName("PK__Dish__856DB3EB959FC6C7");

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

        modelBuilder.Entity<History>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__History__3214EC0751E7FE92");

            entity.Property(e => e.AveragePrice)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CaloriesRange)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DishFive)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DishFour)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DishOne)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DishSix)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DishThree)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DishTwo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Image).IsUnicode(false);
            entity.Property(e => e.UserId)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.History)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_History_JiakUser");
        });

        modelBuilder.Entity<JiakUser>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__JiakUser__1788CC4C3451BC3E");

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
            entity.HasKey(e => e.FoodName).HasName("PK__Menu__81B4FC24589794DE");

            entity.Property(e => e.AverageNv).HasColumnName("AverageNV");
            entity.Property(e => e.AveragePrice).HasColumnType("decimal(18, 2)");
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
            entity.HasKey(e => e.PredictionId).HasName("PK__Predicti__BAE4C1A0061E5E3F");

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
                .HasConstraintName("FK__Predictio__Datas__02E7657A");

            entity.HasOne(d => d.Menu).WithMany(p => p.Prediction)
                .HasForeignKey(d => d.MenuId)
                .HasConstraintName("FK__Predictio__MenuI__01F34141");
        });

        modelBuilder.Entity<Reviews>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__Reviews__74BC79AE48980494");

            entity.Property(e => e.ReviewId).HasColumnName("ReviewID");
            entity.Property(e => e.Comment).IsUnicode(false);
            entity.Property(e => e.ImageData).IsUnicode(false);
            entity.Property(e => e.PublishDate).HasColumnType("date");
        });

        modelBuilder.Entity<Summary>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.BatangFish)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BoiledEgg)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BraisedMeat)
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
            entity.Property(e => e.Omelette)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SteamedEgg)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("UserID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
