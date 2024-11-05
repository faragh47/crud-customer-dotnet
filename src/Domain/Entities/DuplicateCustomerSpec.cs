using CleanArchitecture.Domain.ValueObjects;
using System.Linq.Expressions;

namespace CleanArchitecture.Domain.Entities;

public class DuplicateEmailCustomerSpec : ISpecification<Customer> //usage in implementation of CampaignRepository
{
    private readonly string _Email;

    public DuplicateEmailCustomerSpec(string email)
    {
        _Email = email;
    }

    public Expression<Func<Customer, bool>> IsSatisfiedBy()
    {
        return x => x.Email != null && x.Email.Value.Equals(_Email);
    }
}
public class DuplicateCustomerSpec : ISpecification<Customer> //usage in implementation of CampaignRepository
{
    private readonly string _FirtstName;
    private readonly string _LastName;
    private readonly DateTime? DateOfBirth;
    public DuplicateCustomerSpec(string firtstName, string lastName, DateTime? dateOfBirth)
    {
        _FirtstName = firtstName;
        _LastName = lastName;
        DateOfBirth = dateOfBirth;
    }

    public Expression<Func<Customer, bool>> IsSatisfiedBy()
    {
        return x => x.FirstName != null && x.FirstName.Equals(_FirtstName)
        && x.LastName != null && x.LastName.Equals(_LastName)
        && x.DateOfBirth != null && x.DateOfBirth.Value.Equals(DateOfBirth);
    }
}