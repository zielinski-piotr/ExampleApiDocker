using Microsoft.EntityFrameworkCore;

namespace FirstExampleApi.Data;

public class FirstExampleDbContext : DbContext
{
    public DbSet<FirstExampleEntity> FirstExampleEntities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FirstExampleEntity>()
            .HasKey(x => x.Id);
    }
}