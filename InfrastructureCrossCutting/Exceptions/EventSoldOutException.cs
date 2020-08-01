namespace InfrastructureCrossCutting.Exceptions
{
    using System;

    public class EventSoldOutException : Exception
    {
        public EventSoldOutException()
        {

        }
        public EventSoldOutException(string message) : base(message)
        {
        }

        public EventSoldOutException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
