using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Events;
using CleanArchitecture.Domain.ValueObjects;

namespace CleanArchitecture.Application.Customers.Commands.CreateCustomer;

public record CreateCustomerCommand : IRequest<int>
{
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public DateTime? DateOfBirth { get; set; }
    public ulong PhoneNumber { get; set; }
    public string Email { get; set; } = "";
    public string BankAccountNumber { get; set; } = "";
}

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateCustomerCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var entity = new Customer(request.FirstName,
            request.LastName,
            request.DateOfBirth,
            new PhoneNumber(request.PhoneNumber),
            new Email(request.Email),
            new BankAccountNumber(request.BankAccountNumber));
        entity.AddDomainEvent(new CustomerCreatedEvent(entity));
        _context.Customers.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Id;
    }
}
