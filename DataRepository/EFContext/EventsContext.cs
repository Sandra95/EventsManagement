namespace DataRepository.EFContext
{
    using System;
    using DataRepository.Models;
    using Microsoft.EntityFrameworkCore;

    public class EventsContext : DbContext, IDisposable
    {

        public EventsContext() : base()
        {
        }

        public EventsContext(DbContextOptions<EventsContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>()
                  .Property(x => x.Id)
                  .HasDefaultValue();

            modelBuilder.Entity<Event>()
                .HasMany<EventRegistration>(e => e.EventsRegistrations)
                .WithOne(e => e.Event)
                .HasForeignKey(e => e.EventId);

            modelBuilder.Entity<Registration>()
                 .Property(x => x.Id)
                 .HasDefaultValue();

            modelBuilder.Entity<Registration>()
               .HasMany<EventRegistration>(a => a.EventsRegistrations)
               .WithOne(e => e.Registration)
               .HasForeignKey(e => e.RegistrationId);

            modelBuilder.Entity<EventRegistration>()
                .HasOne<Event>(e => e.Event)
                .WithMany(e => e.EventsRegistrations)
                .HasForeignKey(fk => fk.EventId);

            modelBuilder.Entity<EventRegistration>()
                .HasOne<Registration>(x => x.Registration)
                .WithMany(s => s.EventsRegistrations)
                .HasForeignKey(fk => fk.RegistrationId);

        }

        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Registration> Registrations { get; set; }
        public virtual DbSet<EventRegistration> EventsRegistrations { get; set; }

    }
}
