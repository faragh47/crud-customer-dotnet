using CleanArchitecture.Domain.Events;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.TodoItems.EventHandlers;

public class CustomerCompletedEventHandler : INotificationHandler<CustomerCompletedEvent>
{
    private readonly ILogger<CustomerCompletedEventHandler> _logger;

    public CustomerCompletedEventHandler(ILogger<CustomerCompletedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(CustomerCompletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
