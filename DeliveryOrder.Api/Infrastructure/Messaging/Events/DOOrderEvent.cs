using GoLogs.Events;
using GoLogs.Interfaces;
using MediatR;

// ReSharper disable InconsistentNaming
namespace GoLogs.Services.DeliveryOrder.Api.Infrastructure.Messaging.Events
{
    public abstract class DOOrderEvent : IDOOrderEvent, INotification
    {
        public IDOOrder DOOrder { get; }

        protected DOOrderEvent(IDOOrder doOrder)
        {
            DOOrder = doOrder;
        }
    }
}