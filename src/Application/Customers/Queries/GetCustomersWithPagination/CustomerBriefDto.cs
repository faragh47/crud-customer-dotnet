using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Customers.Queries.GetCustomersWithPagination;

public class CustomerBriefDto
{
    public string? FirstName { get; set; } = "";
    public string? LastName { get; set; } = "";
    public DateTime? DateOfBirth { get; set; }
    public ulong? PhoneNumber { get; set; }
    public string? Email { get; set; } = "";
    public string? BankAccountNumber { get; set; } = "";
}
