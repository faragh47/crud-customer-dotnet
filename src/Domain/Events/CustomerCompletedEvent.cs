namespace CleanArchitecture.Domain.Events;

public class CustomerCompletedEvent : BaseEvent
{
    public CustomerCompletedEvent(Customer item)
    {
        Item = item;
    }

    public Customer Item { get; }
}
