using Microsoft.EntityFrameworkCore;
using PrimeVaultApi.Models;

namespace PrimeVaultApi.Db;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Usuario> Usuario { get; set; }
    public DbSet<Conta> Conta { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Conta>()
            .ToTable("Conta");

        modelBuilder.Entity<Conta>()
            .HasKey(c => c.Id);

        modelBuilder.Entity<Conta>()
            .Property(c => c.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Conta>()
            .Property(c => c.NumeroConta)
            .HasMaxLength(9)
            .IsRequired();

        modelBuilder.Entity<Conta>()
            .Property(c => c.TipoConta)
            .HasMaxLength(255)
            .IsRequired();

        modelBuilder.Entity<Conta>()
            .Property(c => c.Saldo)
            .IsRequired();

        //Config usuario

        modelBuilder.Entity<Usuario>()
            .ToTable("usuario");

        modelBuilder.Entity<Usuario>()
            .HasKey(u => u.Id);

        modelBuilder.Entity<Usuario>()
            .Property(u => u.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Usuario>()
            .Property(u => u.Email)
            .HasMaxLength(255)
            .IsRequired();

        modelBuilder.Entity<Usuario>()
            .Property(u => u.Nome)
            .HasMaxLength(255)
            .IsRequired();

        modelBuilder.Entity<Usuario>()
            .Property(u => u.Senha)
            .HasMaxLength(255)
            .IsRequired();

        modelBuilder.Entity<Conta>()
            .HasOne(c => c.Usuario)
            .WithOne(u => u.Conta)
            .HasForeignKey<Conta>(c => c.User_id)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
            


    }
}
