namespace DataRepository.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Attendee
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public int NIF { get; set; }

        public IEnumerable<EventRegistration> EventsRegistrations { get; set; }
    }
}
