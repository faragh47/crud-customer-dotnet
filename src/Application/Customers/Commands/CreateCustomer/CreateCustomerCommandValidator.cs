using CleanArchitecture.Domain.ValueObjects;

namespace CleanArchitecture.Application.Customers.Commands.CreateCustomer;

public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
        RuleFor(x => x.Email)
           .Must(input => Email.IsValid(input)).WithMessage("Invalid email format.");
        RuleFor(x => x.PhoneNumber)
            .NotNull().WithMessage("PhoneNumber cannot be null.")
           .Must(input => PhoneNumber.IsValid(input.ToString())).WithMessage("Invalid email format.");
        RuleFor(x => x.BankAccountNumber)
           .Must(input => BankAccountNumber.IsValid(input)).WithMessage("Invalid email format.");
    }
}
