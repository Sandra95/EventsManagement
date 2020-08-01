﻿using System.Text;

namespace ApplicationDTO.Validator
{
    public class RegistrationValidator : IValidator<RegistrationDto>
    {
        private RegistrationDto registration;
        private StringBuilder errors;


        public bool Validate(RegistrationDto entity)
        {
            var isValid = true;
            this.errors = new StringBuilder();
            this.registration = entity;

            isValid &= this.ValidateName();
            isValid &= this.ValidateAge();
            isValid &= this.ValidateNif();

            return isValid;
        }

        private bool ValidateNif()
        {
            return (this.registration.AttendeeNif > 0);
        }

        private bool ValidateAge()
        {
            return (this.registration.AttendeeAge > 0);
        }

        private bool ValidateName()
        {
            return (string.IsNullOrWhiteSpace(this.registration.AttendeeName) &&
                this.registration.AttendeeName.Length < 50);
        }

        public string GetErrors()
        {
            return this.errors.ToString();
        }
    }
}