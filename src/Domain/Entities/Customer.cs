namespace CleanArchitecture.Domain.Entities;

public class Customer : BaseAuditableEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public PhoneNumber PhoneNumber { get; set; }
    public Email Email { get; set; }
    public BankAccountNumber BankAccountNumber { get; set; }
    public Customer(string firstName,
                    string lastName,
                    DateTime? dateOfBirth,
                    PhoneNumber phoneNumber,
                    Email email,
                    BankAccountNumber bankAccountNumber)
    {
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        PhoneNumber = phoneNumber;
        Email = email;
        BankAccountNumber = bankAccountNumber;

    }
}
