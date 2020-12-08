// ReSharper disable InconsistentNaming

using GoLogs.Interfaces;

namespace GoLogs.Services.DeliveryOrder.Api.Infrastructure.Messaging.Events
{
    public class DOOrderCreatedEvent : DOOrderEvent
    {
        public DOOrderCreatedEvent(IDOOrder doOrder) : base(doOrder)
        {
        }
    }
}