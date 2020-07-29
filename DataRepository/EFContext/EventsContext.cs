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
            this.SetEventsModel(modelBuilder);
        }

        public virtual DbSet<Event> Events { get; set; }


        private void SetEventsModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>()
                .Property(x => x.Id)
                .HasDefaultValue();
        }
    }
}
