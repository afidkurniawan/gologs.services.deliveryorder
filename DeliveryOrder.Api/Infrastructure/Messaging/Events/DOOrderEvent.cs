// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

using GoLogs.Events;
using GoLogs.Interfaces;
using MediatR;

// ReSharper disable InconsistentNaming
namespace GoLogs.Services.DeliveryOrder.Api.Infrastructure.Messaging.Events
{
    public abstract class DOOrderEvent : IDOOrderEvent, INotification
    {
        protected DOOrderEvent(IDOOrder doOrder)
        {
            DOOrder = doOrder;
        }

        public IDOOrder DOOrder { get; }
    }
}
