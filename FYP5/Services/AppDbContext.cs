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

    public virtual DbSet<BoundingBox> BoundingBox { get; set; }

    public virtual DbSet<Calories> Calories { get; set; }

    public virtual DbSet<Dataset> Dataset { get; set; }

    public virtual DbSet<Dates> Dates { get; set; }

    public virtual DbSet<History> History { get; set; }

    public virtual DbSet<Items> Items { get; set; }

    public virtual DbSet<JiakUser> JiakUser { get; set; }

    public virtual DbSet<LocationPrice> LocationPrice { get; set; }

    public virtual DbSet<Locations> Locations { get; set; }

    public virtual DbSet<Menu> Menu { get; set; }

    public virtual DbSet<Prediction> Prediction { get; set; }

    public virtual DbSet<Reviews> Reviews { get; set; }

    public virtual DbSet<Summary> Summary { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BoundingBox>(entity =>
        {
            entity.HasKey(e => e.BoundingBoxId).HasName("PK__Bounding__8AB42BA026EF29AE");

            entity.HasOne(d => d.Prediction).WithMany(p => p.BoundingBox)
                .HasForeignKey(d => d.PredictionId)
                .HasConstraintName("FK__BoundingB__Predi__236943A5");
        });

        modelBuilder.Entity<Calories>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Calories__3214EC07EA7E253D");
        });

        modelBuilder.Entity<Dataset>(entity =>
        {
            entity.HasKey(e => e.DatasetId).HasName("PK__Dataset__CCE574CB57CC93E7");

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
                .HasConstraintName("FK__Dataset__UserId__1CBC4616");
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
            entity.HasKey(e => e.Id).HasName("PK__History__3214EC074746B2CE");

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

        modelBuilder.Entity<Items>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PK__Items__727E83EBD9D11EFE");

            entity.Property(e => e.ItemId).HasColumnName("ItemID");
            entity.Property(e => e.ItemName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<JiakUser>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__JiakUser__1788CC4CD4FBDD84");

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

        modelBuilder.Entity<LocationPrice>(entity =>
        {
            entity.HasKey(e => new { e.LocationId, e.ItemId }).HasName("PK__Location__40D94C49CD444DD3");

            entity.Property(e => e.LocationId).HasColumnName("LocationID");
            entity.Property(e => e.ItemId).HasColumnName("ItemID");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Item).WithMany(p => p.LocationPrice)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LocationP__ItemI__2B0A656D");

            entity.HasOne(d => d.Location).WithMany(p => p.LocationPrice)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LocationP__Locat__2A164134");
        });

        modelBuilder.Entity<Locations>(entity =>
        {
            entity.HasKey(e => e.LocationId).HasName("PK__Location__E7FEA477AF99085E");

            entity.Property(e => e.LocationId).HasColumnName("LocationID");
            entity.Property(e => e.LocationName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.MenuId).HasName("PK__Menu__C99ED23013A24915");

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
            entity.HasKey(e => e.PredictionId).HasName("PK__Predicti__BAE4C1A09A22AB91");

            entity.Property(e => e.HighestPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.LowestPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TagName)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Dataset).WithMany(p => p.Prediction)
                .HasForeignKey(d => d.DatasetId)
                .HasConstraintName("FK__Predictio__Datas__208CD6FA");

            entity.HasOne(d => d.Menu).WithMany(p => p.Prediction)
                .HasForeignKey(d => d.MenuId)
                .HasConstraintName("FK__Predictio__MenuI__1F98B2C1");
        });

        modelBuilder.Entity<Reviews>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__Reviews__74BC79AE532C14EF");

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
