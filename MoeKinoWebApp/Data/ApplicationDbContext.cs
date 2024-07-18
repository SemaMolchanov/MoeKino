using Microsoft.EntityFrameworkCore;
using MoeKinoWebApp.Models;

namespace MoeKinoWebApp.Data;


public class ApplicationDbContext: DbContext{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){

    }

    public DbSet<Genres> Genres { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<Genres>(entity =>
    {
        entity.ToTable("Genres");
        entity.HasKey(e => e.Id);
        entity.Property(e => e.NameEn).IsRequired().HasMaxLength(50);
        entity.Property(e => e.NameRu).IsRequired().HasMaxLength(50);
    });
}

}