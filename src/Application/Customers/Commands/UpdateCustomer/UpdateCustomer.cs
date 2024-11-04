using CleanArchitecture.Application.Common.Interfaces;

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
        var entity = await _context.Customers
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);
        entity = _mapper.Map(request, entity);
        _context.Customers.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
