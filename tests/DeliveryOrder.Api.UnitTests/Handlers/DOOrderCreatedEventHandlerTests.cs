// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

using System;
using System.Threading;
using System.Threading.Tasks;
using GoLogs.Events;
using GoLogs.Interfaces;
using GoLogs.Services.DeliveryOrder.Api.Infrastructure.Messaging;
using GoLogs.Services.DeliveryOrder.Api.Infrastructure.Messaging.Events;
using MassTransit;
using Moq;
using Xunit;

namespace GoLogs.Services.DeliveryOrder.Api.UnitTests.Handlers
{
    public class DOOrderCreatedEventHandlerTests
    {
        private readonly Mock<IPublishEndpoint> _mockPublishEndpoint;

        public DOOrderCreatedEventHandlerTests()
        {
            _mockPublishEndpoint = new Mock<IPublishEndpoint>();
        }

        [Fact]
        public async Task Handle_EmptyEvent_ThrowsException()
        {
            var handler = new DOOrderCreatedEventHandler(_mockPublishEndpoint.Object);

            await Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(null));
        }

        [Fact]
        public async Task Handle_DOOrderCreated_EventPublishedOnce()
        {
            var order = new Mock<IDOOrder>().Object;
            var createdEvent = new DOOrderCreatedEvent(order);
            var cancellationToken = CancellationToken.None;
            _mockPublishEndpoint
                .Setup(endpoint =>
                    endpoint.Publish<IDOOrderCreatedEvent>(
                        It.Is<object>(
                            o => o.GetType().GetProperty(nameof(DOOrderCreatedEvent.DOOrder)).GetValue(o) == order),
                        cancellationToken))
                .Returns(Task.CompletedTask).Verifiable();
            var handler = new DOOrderCreatedEventHandler(_mockPublishEndpoint.Object);

            await handler.Handle(createdEvent, cancellationToken);

            _mockPublishEndpoint.Verify(
                endpoint =>
                    endpoint.Publish<IDOOrderCreatedEvent>(
                        It.Is<object>(
                            o => o.GetType().GetProperty(nameof(DOOrderCreatedEvent.DOOrder)).GetValue(o) == order),
                        cancellationToken),
                Times.Once());
        }
    }
}
