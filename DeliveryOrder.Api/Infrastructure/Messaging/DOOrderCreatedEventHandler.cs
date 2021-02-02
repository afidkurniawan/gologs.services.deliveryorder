// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

using System.Threading;
using System.Threading.Tasks;
using GoLogs.Events;
using GoLogs.Services.DeliveryOrder.Api.Infrastructure.Messaging.Events;
using MassTransit;
using MediatR;
using Nirbito.Framework.Core;

namespace GoLogs.Services.DeliveryOrder.Api.Infrastructure.Messaging
{
    // ReSharper disable once InconsistentNaming
    public class DOOrderCreatedEventHandler : INotificationHandler<DOOrderCreatedEvent>
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public DOOrderCreatedEventHandler(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task Handle(DOOrderCreatedEvent doOrderCreatedEvent, CancellationToken cancellationToken)
        {
            Check.NotNull(doOrderCreatedEvent, nameof(doOrderCreatedEvent));

            await _publishEndpoint.Publish<IDOOrderCreatedEvent>(
                new { DOOrder = doOrderCreatedEvent.DOOrder },
                cancellationToken);
        }
    }
}
