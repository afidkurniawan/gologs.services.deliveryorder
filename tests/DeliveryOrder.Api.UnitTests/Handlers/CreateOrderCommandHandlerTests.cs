// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

using System;
using System.Threading.Tasks;
using GoLogs.Framework.Mvc;
using GoLogs.Services.DeliveryOrder.Api.Application.Internals;
using GoLogs.Services.DeliveryOrder.Api.Commands;
using Moq;
using Xunit;

namespace GoLogs.Services.DeliveryOrder.Api.UnitTests.Handlers
{
    public class CreateOrderCommandHandlerTests
    {
        private readonly Mock<IDOOrderContext> _mockContext;
        private readonly Mock<IProblemCollector> _mockProblemCollector;

        public CreateOrderCommandHandlerTests()
        {
            _mockContext = new Mock<IDOOrderContext>();
            _mockProblemCollector = new Mock<IProblemCollector>();
        }

        [Fact]
        public async Task Handle_EmptyCommand_ThrowsException()
        {
            var handler = new CreateOrderCommandHandler(_mockContext.Object, _mockProblemCollector.Object);

            await Assert.ThrowsAsync<ArgumentNullException>(() => handler.Handle(null));
        }
    }
}
