using Microservice_TrailerTrak.Model;
using Microsoft.EntityFrameworkCore;

namespace Microservice_TrailerTrak.Contexts
{
    // TrailerTrak Context => Opsætning af vores migration / Database
    public class TrailerTrakContext : DbContext
    {
        public TrailerTrakContext(DbContextOptions<TrailerTrakContext> options)
            : base(options) { }

        // De tabeller vi har i vores database
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Trailer> Trailers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // OnModelCreating => For at vi kan specificere relationer mellem vores tabeller, når vi laver vores migrations
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.CustomerData)
			   .WithMany(c => c.Bookings)
				.HasForeignKey(b => b.CustomerId);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.TrailerData)
				.WithMany(c => c.Bookings)
				.HasForeignKey(b => b.TrailerId);

        }
    }
}
