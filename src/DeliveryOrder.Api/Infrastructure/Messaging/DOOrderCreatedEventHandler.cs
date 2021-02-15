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

    /// <summary>
    /// Initialize public class DOOrderCreatedEventHandler.
    /// </summary>
    public class DOOrderCreatedEventHandler : INotificationHandler<DOOrderCreatedEvent>
    {
        private readonly IPublishEndpoint _publishEndpoint;

        /// <summary>
        /// Initializes a new instance of the <see cref="DOOrderCreatedEventHandler"/> class.
        /// </summary>
        /// <param name="publishEndpoint">Define IPublishEndpoint.</param>
        public DOOrderCreatedEventHandler(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        /// <summary>
        /// Initialize Handle for DOOrderCreatedEvent.
        /// </summary>
        /// <param name="doOrderCreatedEvent">Define DOOrderCreatedEvent.</param>
        /// <param name="cancellationToken">Define CancellationToken.</param>
        /// <returns>DOOrderCreatedEvent.</returns>
        public async Task Handle(DOOrderCreatedEvent doOrderCreatedEvent, CancellationToken cancellationToken = default)
        {
            Check.NotNull(doOrderCreatedEvent, nameof(doOrderCreatedEvent));

            await _publishEndpoint.Publish<IDOOrderCreatedEvent>(
                new { DOOrder = doOrderCreatedEvent.DOOrder },
                cancellationToken);
        }
    }
}
