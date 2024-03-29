﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Valkyrie.Entities;

public class ValkDbContext : IdentityDbContext<IdentityUser>
{
    public ValkDbContext(DbContextOptions<ValkDbContext> options)
        : base(options) { }

    public DbSet<DevBoard> DevBoards { get; set; }
    public DbSet<SBC> SBC { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        _ = modelBuilder.Entity<DevBoard>()
                        .HasIndex(_ => _.Guid)
                        .IsUnique();

        _ = modelBuilder.Entity<SBC>()
                        .HasIndex(_ => _.Guid)
                        .IsUnique();

        
    }
}
