// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

using System;
using GoLogs.Contracts.Events;
using GoLogs.Interfaces;

namespace GoLogs.Services.DeliveryOrder.Api.Events
{
    public class HistoryConfirmedEvent : IHistoryCreatedEvent, IHistoryEvent
    {
        public int StateId { get; set; }

        public string StateName { get; set; }

        public string EventName { get; set; }

        public string Metadata { get; set; }

        public DateTime Created { get; set; }
    }
}
