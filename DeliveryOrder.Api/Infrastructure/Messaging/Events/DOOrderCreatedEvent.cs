// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

using GoLogs.Interfaces;

// ReSharper disable InconsistentNaming
namespace GoLogs.Services.DeliveryOrder.Api.Infrastructure.Messaging.Events
{
    public class DOOrderCreatedEvent : DOOrderEvent
    {
        public DOOrderCreatedEvent(IDOOrder doOrder)
            : base(doOrder)
        {
        }
    }
}
