namespace DataRepository.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Events")]
    public class Event
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        public int MaxAttendance { get; set; }

        public DateTime DueDate { get; set; }

        public virtual IEnumerable<EventRegistration> EventsRegistrations { get; set; }
    }
}
