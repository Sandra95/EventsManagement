namespace ApplicationDTO
{
    using System;

    public class EventDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        public int MaxAttendance { get; set; }

        public DateTime DueDate { get; set; }
    }
}
