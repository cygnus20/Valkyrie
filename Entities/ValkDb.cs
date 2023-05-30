using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Text;

namespace Valkyrie.Entities;

public class ValkDbContext : DbContext
{
    public ValkDbContext(DbContextOptions<ValkDbContext> options)
        : base(options) { }

    public DbSet<DevBoard> DevBoards { get; set; }
    public DbSet<SBC> SBC { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        _ = modelBuilder.Entity<DevBoard>()
                        .HasIndex(_ => _.Guid)
                        .IsUnique();

        _ = modelBuilder.Entity<SBC>()
                        .HasIndex(_ => _.Guid)
                        .IsUnique();

        
    }
}
