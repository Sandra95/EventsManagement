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

        public Guid RegistrationId { get; set; }

        public Registration Registration { get; set; }
    }
}
