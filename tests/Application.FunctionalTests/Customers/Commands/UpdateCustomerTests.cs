using CleanArchitecture.Application.Customers.Commands.CreateCustomer;
using CleanArchitecture.Application.Customers.Commands.UpdateCustomer;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.FunctionalTests.Customers.Commands;

using static Testing;

public class UpdateCustomerTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidCustomerId()
    {
        var command = new UpdateCustomerCommand { Id = 99, FirstName = "New Title" };
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldUpdateCustomer()
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

        var command = new UpdateCustomerCommand
        {
            Id = itemId,
            FirstName = "محمدرضا",
            LastName = "فرقدانی",
            PhoneNumber = 9124798930,
            DateOfBirth = DateTime.Now,
            Email = "farghadani4747@gmail.com",
            BankAccountNumber = "6219861057747882",
        };

        await SendAsync(command);

        var item = await FindAsync<Customer>(itemId);

        item.Should().NotBeNull();
        item!.LastName.Should().Be(command.LastName);
        item.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
