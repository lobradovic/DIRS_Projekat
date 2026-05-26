using DIRS_Ketering.Models;
using Microsoft.EntityFrameworkCore;

namespace DIRS_Ketering.Data;

public class AppDbContext : DbContext
{
    public DbSet<Korisnik> Korisnici { get; set; }
    public DbSet<Jelo> Jela { get; set; }
    public DbSet<Porudzbina> Porudzbine { get; set; }
    public DbSet<Stavka> Stavke { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        var path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ketering.db");
        options.UseSqlite($"Data Source={path}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Korisnik>()
            .Property(k => k.Rola)
            .HasConversion<string>();

        modelBuilder.Entity<Porudzbina>()
            .Property(p => p.Status)
            .HasConversion<string>();

        modelBuilder.Entity<Porudzbina>()
            .Ignore(p => p.Ukupno);


        modelBuilder.Entity<Korisnik>().HasData(
            new Korisnik
            {
                Id = 1,
                Ime = "Admin",
                Email = "admin@admin.com",
                Lozinka = "admin123",
                Rola = Rola.Admin
            }
        );

        modelBuilder.Entity<Jelo>().HasData(
            new Jelo { Id = 1, Naziv = "Plata suhomesnatih proizvoda", Opis = "Idealno predjelo", Cena = 5500 },
            new Jelo { Id = 2, Naziv = "Brusketi sa lososom", Opis = "Finger food za sve vrste proslava", Cena = 200 },
            new Jelo { Id = 3, Naziv = "Cezar salata", Opis = "", Cena = 1500 },
            new Jelo { Id = 4, Naziv = "Pileci stapici", Opis = "Hrskavi pileci stapici", Cena = 1200 }
        );
    }
}