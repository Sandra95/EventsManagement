using System;
using System.Text;

namespace ApplicationDTO.Validator
{
    public class EventDtoValidator : IValidator<EventDto>
    {
        private EventDto eventDto;
        private StringBuilder errors;

        public bool Validate(EventDto eventDto)
        {
            var isValid = true;
            errors = new StringBuilder();

            this.eventDto = eventDto;

            isValid &= this.ValidateName();
            isValid &= this.ValidateDescription();
            isValid &= this.ValidateDueDate();
            isValid &= this.ValidateMaxAttendance();
            isValid &= this.ValidateLocation();

            return isValid;
        }

        public string GetErrors()
        {
            return this.errors.ToString();
        }

        private bool ValidateLocation()
        {
            if (!string.IsNullOrWhiteSpace(this.eventDto.Location) && this.eventDto.Location.Length <= 250)
            {
                return true;
            }

            this.errors.AppendLine($"The field'{nameof(this.eventDto.Location)}' is required and should have a max of 250 characters.");

            return false;
        }

        private bool ValidateMaxAttendance()
        {
            if (this.eventDto.MaxAttendance >= 1)
            {
                return true;
            }

            this.errors.AppendLine($"The field '{nameof(this.eventDto.MaxAttendance)}' is required and should be gratter or equal than 1.");
            return false;
        }

        private bool ValidateDueDate()
        {
            if (this.eventDto.DueDate >= DateTime.Now)
            {
                return true;
            }

            this.errors.AppendLine($"The field '{nameof(this.eventDto.DueDate)}' is required and the event should happen after the current date.");
            return false;
        }

        private bool ValidateDescription()
        {
            if (!string.IsNullOrWhiteSpace(this.eventDto.Description) && this.eventDto.Description.Length <= 250)
            {
                return true;
            }
            this.errors.AppendLine($"The field '{nameof(this.eventDto.Description)}' is required and should have a max of 250 characters.");
            return false;
        }

        private bool ValidateName()
        {
            if (!string.IsNullOrWhiteSpace(this.eventDto.Name)
                && this.eventDto.Name.Length <= 50)
            {
                return true;
            }
            this.errors.AppendLine($"The field '{nameof(this.eventDto.Name)}' is required and should have a max of 50 characters.");
            return false;
        }
    }
}
