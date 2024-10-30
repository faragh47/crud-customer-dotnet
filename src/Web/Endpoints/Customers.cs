using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Customers.Commands.CreateCustomer;
using CleanArchitecture.Application.Customers.Commands.DeleteCustomer;
using CleanArchitecture.Application.Customers.Commands.UpdateCustomer;
using CleanArchitecture.Application.Customers.Queries.GetCustomersWithPagination;

namespace CleanArchitecture.Web.Endpoints;

public class Customers : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetCustomersWithPagination)
            .MapPost(CreateCustomer)
            .MapPut(UpdateCustomer, "{id}")
            .MapDelete(DeleteCustomer, "{id}");
    }

    public Task<PaginatedList<CustomerBriefDto>> GetCustomersWithPagination(ISender sender, [AsParameters] GetCustomersWithPaginationQuery query)
    {
        return sender.Send(query);
    }

    public Task<int> CreateCustomer(ISender sender, CreateCustomerCommand command)
    {
        return sender.Send(command);
    }

    public async Task<IResult> UpdateCustomer(ISender sender, int id, UpdateCustomerCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }

    public async Task<IResult> DeleteCustomer(ISender sender, int id)
    {
        await sender.Send(new DeleteCustomerCommand(id));
        return Results.NoContent();
    }
}
