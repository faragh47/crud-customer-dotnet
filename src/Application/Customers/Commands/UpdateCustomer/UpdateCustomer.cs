using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.ValueObjects;

namespace CleanArchitecture.Application.Customers.Commands.UpdateCustomer;

public record UpdateCustomerCommand : IRequest
{
    public int Id { get; init; }

    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public DateTime? DateOfBirth { get; set; }
    public ulong PhoneNumber { get; set; }
    public string Email { get; set; } = "";
    public string BankAccountNumber { get; set; } = "";
}

public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UpdateCustomerCommandHandler(IApplicationDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var entity =  await _context.Customers
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);
        entity.PhoneNumber=new PhoneNumber(request.PhoneNumber);
        entity.Email= new Email(request.Email);
        entity.FirstName=request.FirstName;
        entity.LastName=request.LastName;
        entity.BankAccountNumber=new BankAccountNumber(request.BankAccountNumber);
        entity.DateOfBirth=request.DateOfBirth;
        _context.Customers.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
