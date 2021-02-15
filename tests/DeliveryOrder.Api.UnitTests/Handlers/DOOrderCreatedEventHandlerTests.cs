// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

using System;
using System.Threading.Tasks;
using GoLogs.Services.DeliveryOrder.Api.Infrastructure.Messaging;
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
    }
}
