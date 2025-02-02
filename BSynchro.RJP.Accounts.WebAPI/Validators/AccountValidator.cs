using BSynchro.RJP.Accounts.WebAPI.Models.Requests.Accounts;
using FluentValidation;
using Microsoft.AspNetCore.DataProtection;

namespace BSynchro.RJP.Accounts.WebAPI.Validators
{
    public class AccountValidator : AbstractValidator<OpenAccountRequest>
    {
        private readonly IDataProtector _dataProtector;

        public AccountValidator(IDataProtector dataProtector)
        {
            _dataProtector = dataProtector;

            RuleFor(a => a.CustomerId)
                .NotEmpty()
                .WithMessage("Customer Id is required.")
                .Must(ValidateCustomerId)
                .WithMessage("Invalid customer Id or data tampering detected.");

            RuleFor(a => a.InitialCredit)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Initial credit can't be negative.");
        }

        private bool ValidateCustomerId(string customerId)
        {
            try
            {
                var unprotectedCustomerId = _dataProtector.Unprotect(customerId);
                return int.TryParse(unprotectedCustomerId, out _);  // Check if unprotected Id is a valid integer
            }
            catch
            {
                return false;  // If unprotection fails (e.g., tampered data), return false
            }
        }
    }
}
