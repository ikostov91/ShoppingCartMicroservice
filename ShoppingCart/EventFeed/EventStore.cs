using ShoppingCartNamespace.Contracts;

namespace ShoppingCartNamespace.EventFeed
{
    public class EventStore : IEventStore
    {
        private readonly HashSet<Event> _events = new HashSet<Event>();

        public IEnumerable<Event> GetEvents(long firstEventSequenceNumber, long lastEventSequenceNumber)
        {
            return this._events.Where(x => x.SequenceNumber >= firstEventSequenceNumber && x.SequenceNumber <= lastEventSequenceNumber);
        }

        public void Raise(string eventName, object content)
        {
            var lastEventSequenceNumber = this._events.MaxBy(x => x.SequenceNumber)?.SequenceNumber ?? 0;
            this._events.Add(new Event(lastEventSequenceNumber, DateTimeOffset.Now, eventName, content));
        }
    }
}
