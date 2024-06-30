using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Identity.Client;

namespace FinalProjectAviation.Data
{
    public class AviationDBDbContext : DbContext
    {
        public AviationDBDbContext() { }

        public AviationDBDbContext(DbContextOptions<AviationDBDbContext> options) : base(options) { }
            public virtual DbSet<User> Users { get; set; }
            public virtual DbSet<Flight> Flights { get; set; }
            public virtual DbSet<Passenger> Passengers { get; set; }
            public virtual DbSet<Pilot> Pilots { get; set; }


            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
            modelBuilder.Entity<Passenger>(entity =>
            {
                entity.ToTable("PASSENGERS");

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Ethnicity).HasMaxLength(50).HasColumnName("ETHNICITY");
                entity.Property(e => e.PhoneNumber).HasMaxLength(10).HasColumnName("PHONENUMBER");

                entity.HasOne(d => d.User).WithOne(p => p.Passenger).HasForeignKey<Passenger>(d => d.UserId).HasConstraintName("FK_PASSENGERS_USERS");

                entity.HasMany(s => s.Flights).WithMany(c => c.Passengers).UsingEntity("PASSENGERS_FLIGHTS");
            });


            modelBuilder.Entity<User>(entity =>
               {

                   entity.ToTable("USERS");

                   entity.HasIndex(e => e.Lastname, "IX_LASTNAME");
                   entity.HasIndex(e => e.Email, "UQ_EMAIL").IsUnique();
                   entity.HasIndex(e => e.Username, "UQ_USERNAME").IsUnique();

                   entity.Property(e => e.Id).HasColumnName("ID");
                   entity.Property(e => e.Email).HasMaxLength(50).HasColumnName("EMAIL");
                   entity.Property(e => e.Lastname).HasMaxLength(50).HasColumnName("LASTNAME");
                   entity.Property(e => e.Username).HasMaxLength(50).HasColumnName("USERNAME");
                   entity.Property(e => e.Password).HasMaxLength(512).HasColumnName("PASSWORD");
                   entity.Property(e => e.Firstname).HasMaxLength(50).HasColumnName("FIRSTNAME");
                   entity.Property(e => e.UserRole).HasColumnName("USERROLE").HasConversion<string>().HasMaxLength(50).IsRequired();
               });


                modelBuilder.Entity<Pilot>(entity =>
                {
                    entity.ToTable("PILOTS");

                    entity.Property(e => e.Id).HasColumnName("ID");
                    entity.Property(e => e.Ethnicity).HasMaxLength(50).HasColumnName("ETHNICITY");
                    entity.Property(e => e.PhoneNumber).HasMaxLength(10).HasColumnName("PHONENUMBER");
                    

                    entity.HasOne(d => d.User).WithOne(p => p.Pilot).HasForeignKey<Pilot>(d => d.UserId).HasConstraintName("FK_PILOTS_USERS");

                });


            


                modelBuilder.Entity<Flight>(entity =>
                {
                    entity.ToTable("FLIGHTS");

                    entity.Property(e => e.Id).HasColumnName("ID");
                    entity.Property(e => e.FromCity).HasColumnName("FROMCITY");
                    entity.Property(e => e.ToCity).HasColumnName("TOCITY");
                    entity.Property(e => e.TicketPrice).HasColumnName("TICKETPRICE");

                    entity.HasOne(c => c.Pilot).WithMany(t => t.Flights).HasForeignKey(c => c.PilotId).HasConstraintName("FK_PILOTS_FLIGHTS");

                    entity.HasMany(f => f.Passengers).WithMany(g => g.Flights).UsingEntity("PASSENGERS_FLIGHTS");


                });

            }
         
    }  
}
