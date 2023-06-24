using static Vidly.Utilities.Constants;

namespace Vidly.Models
{
    public class Min18YearsIfAMember :ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            // object instance that gives access to the containing class (Customer)
            var customer = (Customer)validationContext.ObjectInstance;

            if(customer.MembershipTypeId is (byte)MembershipTypes.Unknown or (byte)MembershipTypes.PayAsYouGo)
                return ValidationResult.Success;

            if(customer.Birthdate == null)
                return new ValidationResult("Birthdate is required");

            var age = DateTime.Today.Year - customer.Birthdate.Value.Year;

            return (age >= 18) ? ValidationResult.Success : new ValidationResult("Customer should be at least the age of 18 to sign up for a membership.");
        }
    }
}
