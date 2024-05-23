namespace Zadanie5APBD.Models;

using Microsoft.EntityFrameworkCore;

    public class TripContext : DbContext
    {
        public TripContext(DbContextOptions<TripContext> options)
            : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientTrip> ClientTrips { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<CountryTrip> CountryTrips { get; set; }
        public DbSet<Trip> Trips { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.IdClient);
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(120);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(120);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(120);
                entity.Property(e => e.Telephone).IsRequired().HasMaxLength(120);
                entity.Property(e => e.Pesel).IsRequired().HasMaxLength(120);
            });

            modelBuilder.Entity<ClientTrip>(entity =>
            {
                entity.HasKey(e => new { e.IdClient, e.IdTrip });
                entity.Property(e => e.RegisteredAt).HasColumnType("datetime");
                entity.Property(e => e.PaymentDate).HasColumnType("datetime");
                entity.HasOne(d => d.Client)
                    .WithMany(p => p.ClientTrips)
                    .HasForeignKey(d => d.IdClient)
                    .OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(d => d.Trip)
                    .WithMany(p => p.ClientTrips)
                    .HasForeignKey(d => d.IdTrip)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(e => e.IdCountry);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(120);
            });

            modelBuilder.Entity<CountryTrip>(entity =>
            {
                entity.HasKey(e => new { e.IdCountry, e.IdTrip });
                entity.HasOne(d => d.Country)
                    .WithMany(p => p.CountryTrips)
                    .HasForeignKey(d => d.IdCountry)
                    .OnDelete(DeleteBehavior.ClientSetNull);
                entity.HasOne(d => d.Trip)
                    .WithMany(p => p.CountryTrips)
                    .HasForeignKey(d => d.IdTrip)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Trip>(entity =>
            {
                entity.HasKey(e => e.IdTrip);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(120);
                entity.Property(e => e.Description).IsRequired().HasMaxLength(220);
                entity.Property(e => e.DateFrom).HasColumnType("datetime");
                entity.Property(e => e.DateTo).HasColumnType("datetime");
            });
        }
    }
