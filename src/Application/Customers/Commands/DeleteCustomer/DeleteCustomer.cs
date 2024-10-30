using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Events;

namespace CleanArchitecture.Application.Customers.Commands.DeleteCustomer;

public record DeleteCustomerCommand(int Id) : IRequest;

public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteCustomerCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Customers
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        _context.Customers.Remove(entity);

        entity.AddDomainEvent(new CustomerDeletedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);
    }

}
