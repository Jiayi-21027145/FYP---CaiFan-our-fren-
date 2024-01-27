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

    //public virtual DbSet<ImageUploads> ImageUploads { get; set; }

    public virtual DbSet<JiakUser> JiakUser { get; set; }

    public virtual DbSet<Menu> Menu { get; set; }

    public virtual DbSet<Reviews> Reviews { get; set; }

    public virtual DbSet<UserHistory> UserHistory { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Calories>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Calories__3214EC07B534E6C4");
        });

        /*modelBuilder.Entity<ImageUploads>(entity =>
        {
            entity.HasKey(e => e.UploadId).HasName("PK__ImageUpl__6D16C86DD38C4813");

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
        });*/

        modelBuilder.Entity<JiakUser>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__JiakUser__1788CC4C4DAD0571");

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
            entity.HasNoKey();

            entity.Property(e => e.AverageNv).HasColumnName("AverageNV");
            entity.Property(e => e.AveragePrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.FoodName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.HighestNv).HasColumnName("HighestNV");
            entity.Property(e => e.HighestPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ImageData)
                .HasMaxLength(70)
                .IsUnicode(false);
            entity.Property(e => e.LowestNv).HasColumnName("LowestNV");
            entity.Property(e => e.LowestPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.NutrientName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Reviews>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__Reviews__74BC79AED5653530");

            entity.Property(e => e.ReviewId).HasColumnName("ReviewID");
            entity.Property(e => e.Comment).IsUnicode(false);
            entity.Property(e => e.ImageData).IsUnicode(false);
            entity.Property(e => e.PublishDate).HasColumnType("date");
        });

        modelBuilder.Entity<UserHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserHist__3214EC079AD5D561");

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
