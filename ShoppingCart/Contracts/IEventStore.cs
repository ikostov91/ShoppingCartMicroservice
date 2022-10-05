using ShoppingCartNamespace.EventFeed;

namespace ShoppingCartNamespace.Contracts
{
    public interface IEventStore
    {
        IEnumerable<Event> GetEvents(long firstEventSequenceNumber, long lastEventSequenceNumber);

        void Raise(string eventName, object content);
    }
}
