// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

using GoLogs.Interfaces;

// ReSharper disable InconsistentNaming
namespace GoLogs.Services.DeliveryOrder.Api.Infrastructure.Messaging.Events
{
    /// <summary>
    /// Initialize DOOrderCreatedEvent class.
    /// </summary>
    public class DOOrderCreatedEvent : DOOrderEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DOOrderCreatedEvent"/> class.
        /// </summary>
        /// <param name="doOrder">Define IDOOrder.</param>
        public DOOrderCreatedEvent(IDOOrder doOrder)
            : base(doOrder)
        {
        }
    }
}
