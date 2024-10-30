using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Customers.Queries.GetCustomersWithPagination;

public class CustomerBriefDto
{
    public int Id { get; init; }

    public int ListId { get; init; }

    public string? Title { get; init; }

    public bool Done { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Customer, CustomerBriefDto>();
        }
    }
}
