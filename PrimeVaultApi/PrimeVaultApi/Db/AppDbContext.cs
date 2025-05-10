using Microsoft.EntityFrameworkCore;
using PrimeVaultApi.Models;

namespace PrimeVaultApi.Db;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Conta> Accounts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Conta>()
            .ToTable("Accounts");   
    }
}
