using GoLogs.Contracts.Events;
using GoLogs.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoLogs.Services.DeliveryOrder.Api.Infrastructure.Messaging.Events
{
    public abstract class HistoryEvent : IHistoryEvent, INotification
    {
        public IHistory History { get; }
        protected HistoryEvent(IHistory history)
        {
            History = history;
        }
    }
}
