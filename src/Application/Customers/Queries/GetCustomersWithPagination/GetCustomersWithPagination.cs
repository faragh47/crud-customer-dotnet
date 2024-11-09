using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Application.Common.Models;

namespace CleanArchitecture.Application.Customers.Queries.GetCustomersWithPagination;

public record GetCustomersWithPaginationQuery : IRequest<PaginatedList<CustomerBriefDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetCustomersWithPaginationQueryHandler : IRequestHandler<GetCustomersWithPaginationQuery, PaginatedList<CustomerBriefDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCustomersWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<CustomerBriefDto>> Handle(GetCustomersWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var customers = await _context.Customers
             .OrderBy(x => x.Created)
             .PaginatedListAsync(request.PageNumber, request.PageSize);

        List<CustomerBriefDto> items = new();
        foreach (var item in customers.Items)
        {
            items.Add(new CustomerBriefDto
            { 
                FirstName=item?.FirstName,
                LastName=item?.LastName,
                BankAccountNumber=item?.BankAccountNumber?.Value,
                Email=item?.Email?.Value,
                DateOfBirth=item?.DateOfBirth,
                PhoneNumber=item?.PhoneNumber?.Value
            });
        }
        PaginatedList<CustomerBriefDto> paginatedList =
            new PaginatedList<CustomerBriefDto>(items, customers.TotalCount, request.PageNumber, request.PageSize);
        return paginatedList;
    }
}