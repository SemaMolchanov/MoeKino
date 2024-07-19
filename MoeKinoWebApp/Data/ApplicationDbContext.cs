using Microsoft.EntityFrameworkCore;
using MoeKinoWebApp.Models;

namespace MoeKinoWebApp.Data;


public class ApplicationDbContext: DbContext{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){

    }

    public DbSet<Genre> Genres { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<MovieGenre> MovieGenres { get; set; }
    public DbSet<MovieImage> MovieImages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.ToTable("Genres");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.NameEn).IsRequired().HasMaxLength(50);
            entity.Property(e => e.NameRu).IsRequired().HasMaxLength(50);
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.ToTable("Movies");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.TitleEn).IsRequired().HasMaxLength(160);
            entity.Property(e => e.TitleRu).IsRequired().HasMaxLength(160);
            entity.Property(e => e.DescriptionEn).IsRequired().HasColumnType("nvarchar(MAX)");
            entity.Property(e => e.DescriptionRu).IsRequired().HasColumnType("nvarchar(MAX)");
            entity.Property(e => e.TrailerLinkEn).IsRequired(false).HasMaxLength(2000); 
            entity.Property(e => e.TrailerLinkRu).IsRequired(false).HasMaxLength(2000); 
        });
        
        modelBuilder.Entity<MovieGenre>(entity =>
        {
            entity.ToTable("MovieGenres");
            entity.HasKey(e => new { e.MovieID, e.GenreID });

            entity.HasOne(e => e.Movie)
                .WithMany(m => m.MovieGenres)
                .HasForeignKey(e => e.MovieID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Genre)
                .WithMany(g => g.MovieGenres)
                .HasForeignKey(e => e.GenreID)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<MovieImage>(entity =>
        {
            entity.ToTable("MovieImages");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Image).IsRequired();
            entity.Property(e => e.IsPoster).IsRequired();
            entity.HasOne(e => e.Movie)
                  .WithMany(m => m.MovieImages)
                  .HasForeignKey(e => e.MovieId)
                  .IsRequired()
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }

}