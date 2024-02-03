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

    public virtual DbSet<Dates> Dates { get; set; }

    public virtual DbSet<History> History { get; set; }

    public virtual DbSet<JiakUser> JiakUser { get; set; }

    public virtual DbSet<Locations> Locations { get; set; }

    public virtual DbSet<Menu> Menu { get; set; }

    public virtual DbSet<Prediction> Prediction { get; set; }

    public virtual DbSet<Reviews> Reviews { get; set; }



   /* public virtual DbSet<Summary> Items { get; set; }*/
    public virtual DbSet<Summary> ItemID { get; set; }

   /* public virtual DbSet<Summary> Locations { get; set; }*/

    public virtual DbSet<Summary> Location { get; set; }
   /* public virtual DbSet<Summary> LocationPrice { get; set; } */

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      
        modelBuilder.Entity<Calories>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Calories__3214EC07F4A825F3");
        });

        modelBuilder.Entity<Dataset>(entity =>
        {
            entity.HasKey(e => e.DatasetId).HasName("PK__Dataset__CCE574CB51312185");

            entity.Property(e => e.DateTime).HasColumnType("datetime");
            entity.Property(e => e.Location)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Picture)
                .HasMaxLength(70)
                .IsUnicode(false);
            entity.Property(e => e.UserId)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.User).WithMany(p => p.Dataset)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Dataset__UserId__4E53A1AA");
        });

        modelBuilder.Entity<Dates>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Date)
                .HasColumnType("date")
                .HasColumnName("date");
        });

        modelBuilder.Entity<History>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__History__3214EC0750C82445");

            entity.Property(e => e.AveragePrice).HasColumnType("decimal(18, 2)");
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
            entity.Property(e => e.Location)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PriceRange)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UploadDate).HasColumnType("date");
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
            entity.HasKey(e => e.UserId).HasName("PK__JiakUser__1788CC4C44450890");

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

      

        modelBuilder.Entity<Locations>(entity =>
        {
            entity.HasKey(e => e.LocationId).HasName("PK__Location__E7FEA4773560C508");

            entity.Property(e => e.LocationId).HasColumnName("LocationID");
            entity.Property(e => e.LocationName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });
        
        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.MenuId).HasName("PK__Menu__C99ED230F2D21502");

            entity.Property(e => e.AveragePrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.FoodName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.HighestPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ImageData).HasColumnType("text");
            entity.Property(e => e.LowestPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.NutrientName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Prediction>(entity =>
        {
            entity.HasKey(e => e.PredictionId).HasName("PK__Predicti__BAE4C1A0B971BECB");

            entity.Property(e => e.HighestPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.LowestPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TagName)
                .HasMaxLength(255)
                .IsUnicode(false);

            //entity.HasOne(d => d.Dataset).WithMany(p => p.Prediction)
            //    .HasForeignKey(d => d.DatasetId)
            //    .HasConstraintName("FK__Predictio__Datas__5224328E");

            //entity.HasOne(d => d.Menu).WithMany(p => p.Prediction)
            //    .HasForeignKey(d => d.MenuId)
            //    .HasConstraintName("FK__Predictio__MenuI__51300E55");
        });

        modelBuilder.Entity<Reviews>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__Reviews__74BC79AEF2F03BD5");

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
            entity.Property(e => e.Item)
                .HasColumnName("items");
            entity.Property(e => e.Location)
                 .HasMaxLength(30);

        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
