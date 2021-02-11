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
    /// <summary>
    /// Initialize abstract class DOOrderEvent.
    /// </summary>
    public abstract class DOOrderEvent : IDOOrderEvent, INotification
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DOOrderEvent"/> class.
        /// </summary>
        /// <param name="doOrder">Define IDOOrder.</param>
        protected DOOrderEvent(IDOOrder doOrder)
        {
            DOOrder = doOrder;
        }

        /// <inheritdoc/>
        public IDOOrder DOOrder { get; }
    }
}
