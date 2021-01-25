using GoLogs.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoLogs.Services.DeliveryOrder.Api.Infrastructure.Messaging.Events
{
    public class HistoryCreatedEvent : HistoryEvent
    {
        public HistoryCreatedEvent(IHistory history) : base(history) { 
        }
    }
}
