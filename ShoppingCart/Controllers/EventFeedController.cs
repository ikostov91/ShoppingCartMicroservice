using Microsoft.AspNetCore.Mvc;
using ShoppingCartNamespace.Contracts;
using ShoppingCartNamespace.EventFeed;

namespace ShoppingCartNamespace.Controllers
{
    [Route("/events")]
    public class EventFeedController : ControllerBase
    {
        private readonly IEventStore _eventStore;

        public EventFeedController(IEventStore eventStore)
        {
            this._eventStore = eventStore;
        }

        [HttpGet("")]
        public Event[] Get([FromQuery] long start, [FromQuery] long end = long.MaxValue)
        {
            return this._eventStore.GetEvents(start, end).ToArray();
        }
    }
}
