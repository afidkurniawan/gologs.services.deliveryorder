using GoLogs.Contracts.Events;
using GoLogs.Services.DeliveryOrder.Api.Infrastructure.Messaging.Events;
using MassTransit;
using MediatR;
using Nirbito.Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GoLogs.Services.DeliveryOrder.Api.Infrastructure.Messaging
{
    public class HistoryCreatedEventHandler : INotificationHandler<HistoryCreatedEvent>
    {
        private readonly IPublishEndpoint _publishEndpoint;
        public HistoryCreatedEventHandler(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }
        public async  Task Handle(HistoryCreatedEvent historyCreatedEvent, CancellationToken cancellationToken)
        {
            Check.NotNull(historyCreatedEvent, nameof(historyCreatedEvent));
            await _publishEndpoint.Publish<IHistoryCreatedEvent>(new { History = historyCreatedEvent.History },
                cancellationToken);
        }
    }
}
