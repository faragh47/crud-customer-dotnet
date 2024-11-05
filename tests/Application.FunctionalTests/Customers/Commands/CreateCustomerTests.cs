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
        var command = new CreateCustomerCommand
        {
            FirstName = "محمدرضا",
            LastName = "محمدرضا",
            PhoneNumber = 9124798930,
            DateOfBirth = DateTime.Now,
            Email = "farghadani4747@gmail.com",
            BankAccountNumber = "6219861057747882",
        };

        var itemId = await SendAsync(command);

        var item = await FindAsync<Customer>(itemId);

        item.Should().NotBeNull();
        item?.FirstName.Should().Be(command.FirstName);
        item?.Created.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
        item?.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
