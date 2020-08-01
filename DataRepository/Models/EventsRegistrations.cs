namespace DataRepository.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    public class EventRegistration
    {
        [Key]
        public Guid Id { get; set; }

        public Guid EventId { get; set; }

        public Event Event { get; set; }

        public Guid AttendeeId { get; set; }

        public Attendee Attendee { get; set; }
    }
}
