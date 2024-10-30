using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Customers.Commands.CreateCustomer;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.FunctionalTests.Customers.Commands;

using static Testing;

public class CreateCustomerTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new CreateCustomerCommand();

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldCreateCustomer()
    {
        var listId = await SendAsync(new CreateCustomerCommand
        {
            Title = "New List"
        });

        var command = new CreateCustomerCommand
        {
            ListId = listId,
            Title = "Tasks"
        };

        var itemId = await SendAsync(command);

        var item = await FindAsync<Customer>(itemId);

        item.Should().NotBeNull();
        item!.ListId.Should().Be(command.ListId);
        item.Title.Should().Be(command.Title);
        item.Created.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
        item.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
