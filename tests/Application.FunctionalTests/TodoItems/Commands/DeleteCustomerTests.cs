using CleanArchitecture.Application.Customers.Commands.CreateCustomer;
using CleanArchitecture.Application.Customers.Commands.DeleteCustomer;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.FunctionalTests.Customers.Commands;

using static Testing;

public class DeleteCustomerTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidCustomerId()
    {
        var command = new DeleteCustomerCommand(99);

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldDeleteCustomer()
    {
        var itemId = await SendAsync(new CreateCustomerCommand
        {

            FirstName = "محمدرضا",
            LastName = "محمدرضا",
            PhoneNumber = 9124798930,
            DateOfBirth = DateTime.Now,
            Email = "farghadani4747@gmail.com",
            BankAccountNumber = "6219861057747882",
        });

        await SendAsync(new DeleteCustomerCommand(itemId));
        var item = await FindAsync<Customer>(itemId);
        item.Should().BeNull();
    }
}
