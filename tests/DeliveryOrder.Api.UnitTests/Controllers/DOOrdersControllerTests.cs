// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using GoLogs.Framework.Mvc;
using GoLogs.Interfaces;
using GoLogs.Services.DeliveryOrder.Api.Commands;
using GoLogs.Services.DeliveryOrder.Api.Controllers;
using GoLogs.Services.DeliveryOrder.Api.Models;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Moq;
using Xunit;
using DOQueries = GoLogs.Services.DeliveryOrder.Api.Queries;

namespace GoLogs.Services.DeliveryOrder.Api.UnitTests.Controllers
{
    public class DOOrdersControllerTests
    {
        // ReSharper disable once InconsistentNaming
        private static readonly string[] _doOrderNumbers =
            {
                Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString()
            };

        private readonly Mock<IProblemCollector> _mockProblemCollector;
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<IPublishEndpoint> _mockPublishEndpoint;
        private readonly Mock<IMediator> _mockMediator;

        public DOOrdersControllerTests()
        {
            _mockProblemCollector = new Mock<IProblemCollector>();
            _mockMapper = new Mock<IMapper>();
            _mockPublishEndpoint = new Mock<IPublishEndpoint>();
            _mockMediator = new Mock<IMediator>();
        }

        private static List<DOOrder> TestDOOrders { get; } = new ()
        {
            new DOOrder { Id = 1, DOOrderNumber = _doOrderNumbers[0], CargoOwnerId = 1 },
            new DOOrder { Id = 2, DOOrderNumber = _doOrderNumbers[1], CargoOwnerId = 1 },
            new DOOrder { Id = 3, DOOrderNumber = _doOrderNumbers[2], CargoOwnerId = 2 }
        };

        [Fact]
        public async Task GetAsync_ValidId_ReturnsDOOrder()
        {
            var order = TestDOOrders[0];
            const int DOOrderId = 1;

            _mockMediator
                .Setup(m => m.Send(
                    It.Is<DOQueries.GetById.Request>(req => req.Id == DOOrderId),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => order);
            var controller = new DOOrdersController(_mockProblemCollector.Object, _mockMapper.Object,
                _mockPublishEndpoint.Object, _mockMediator.Object);

            var result = await controller.GetAsync(DOOrderId);

            Assert.IsType<ActionResult<IDOOrder>>(result);
            Assert.Equal(order, ((OkObjectResult)result.Result).Value);
        }

        [Fact]
        public async Task GetAsync_InvalidId_ReturnsBadRequest()
        {
            var controller = new DOOrdersController(_mockProblemCollector.Object, _mockMapper.Object,
                _mockPublishEndpoint.Object, _mockMediator.Object);

            var result = await controller.GetAsync(-1);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task GetAsync_IdNotExists_ReturnsNotFound()
        {
            var controller = new DOOrdersController(_mockProblemCollector.Object, _mockMapper.Object,
                _mockPublishEndpoint.Object, _mockMediator.Object);

            var result = await controller.GetAsync(4);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetAsync_ValidDONumber_ReturnsDOOrder()
        {
            const int DOOrderIdx = 0;
            var orderNumber = _doOrderNumbers[DOOrderIdx];
            var order = TestDOOrders[DOOrderIdx];

            _mockMediator
                .Setup(m => m.Send(
                    It.Is<DOQueries.GetByNumber.Request>(req => req.DoNumber == orderNumber),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => order);
            var controller = new DOOrdersController(_mockProblemCollector.Object, _mockMapper.Object,
                _mockPublishEndpoint.Object, _mockMediator.Object);

            var result = await controller.GetAsync(orderNumber);

            Assert.IsType<ActionResult<IDOOrder>>(result);
            Assert.Equal(order, ((OkObjectResult)result.Result).Value);
        }

        [Fact]
        public async Task GetAsync_DONumberNotExists_ReturnsNotFound()
        {
            var controller = new DOOrdersController(_mockProblemCollector.Object, _mockMapper.Object,
                _mockPublishEndpoint.Object, _mockMediator.Object);

            var result = await controller.GetAsync("1");

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetAsync_ReturnsAll()
        {
            const int PageSize = 3;

            _mockMediator
                .Setup(m => m.Send(
                    It.Is<DOQueries.GetList.Request>(req => req.PageSize == PageSize),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => TestDOOrders);
            var controller = new DOOrdersController(_mockProblemCollector.Object, _mockMapper.Object,
                _mockPublishEndpoint.Object, _mockMediator.Object);

            var result = await controller.GetAsync(null, 1, PageSize);

            Assert.IsType<ActionResult<IList<IDOOrder>>>(result);
            Assert.Equal((int)HttpStatusCode.OK, ((OkObjectResult)result.Result).StatusCode);
            Assert.Equal(TestDOOrders, ((OkObjectResult)result.Result).Value);
        }

        [Fact]
        public async Task GetAsync_ValidCargoOwnerId_ReturnsDOOrders()
        {
            const int CargoOwnerId = 1;

            var doOrdersReturn = new List<DOOrder> { TestDOOrders[0], TestDOOrders[1] };
            _mockMediator
                .Setup(m => m.Send(
                    It.Is<DOQueries.GetListByCargoOwnerId.Request>(req => req.CargoOwnerId == CargoOwnerId),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => doOrdersReturn);
            var controller = new DOOrdersController(_mockProblemCollector.Object, _mockMapper.Object,
                _mockPublishEndpoint.Object, _mockMediator.Object);

            var result = await controller.GetAsync(CargoOwnerId, 1, 3);

            Assert.IsType<ActionResult<IList<IDOOrder>>>(result);
            Assert.Equal(doOrdersReturn, ((OkObjectResult)result.Result).Value);
        }

        [Fact]
        public async Task GetAsync_ValidCargoOwnerId_ReturnsPagedDOOrders()
        {
            const int CargoOwnerId = 1;
            const int Page = 1;
            const int PageSize = 1;

            var doOrdersReturn = new List<DOOrder> { TestDOOrders[0] };
            _mockMediator
                .Setup(m => m.Send(
                    It.Is<DOQueries.GetListByCargoOwnerId.Request>(req =>
                        req.CargoOwnerId == CargoOwnerId && req.Page == Page && req.PageSize == PageSize),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => doOrdersReturn);
            var controller = new DOOrdersController(_mockProblemCollector.Object, _mockMapper.Object,
                _mockPublishEndpoint.Object, _mockMediator.Object);

            var result = await controller.GetAsync(CargoOwnerId, Page, PageSize);

            Assert.IsType<ActionResult<IList<IDOOrder>>>(result);
            Assert.Equal(doOrdersReturn, ((OkObjectResult)result.Result).Value);
        }

        [Fact]
        public async Task GetAsync_InvalidParameters_ReturnsBadRequest()
        {
            var controller = new DOOrdersController(_mockProblemCollector.Object, _mockMapper.Object,
                _mockPublishEndpoint.Object, _mockMediator.Object);

            var result = await controller.GetAsync(-1, -1, -3);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task CreateAsync_ValidCommand_ReturnsCreatedAtAction()
        {
            const int NewId = 4;
            const int CargoOwnerId = 1;

            var createOrderCommand = new CreateOrderCommand { CargoOwnerId = CargoOwnerId };
            var mockUrlHelper = new Mock<IUrlHelper>();
            mockUrlHelper.Setup(url => url.Action(It.IsAny<UrlActionContext>())).Returns("MockUrl");
            _mockMediator
                .Setup(m => m.Send(
                    It.Is<CreateOrderCommand>(cmd => cmd.CargoOwnerId == CargoOwnerId),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(() => new DOOrder { Id = NewId, CargoOwnerId = CargoOwnerId });
            var controller = new DOOrdersController(_mockProblemCollector.Object, _mockMapper.Object,
                _mockPublishEndpoint.Object, _mockMediator.Object)
            { Url = mockUrlHelper.Object };

            var result = await controller.CreateAsync(createOrderCommand);

            Assert.IsType<CreatedAtActionResult>(result);
            Assert.IsType<DOOrder>(((CreatedAtActionResult)result).Value);
            Assert.Equal(NewId, ((DOOrder)((CreatedAtActionResult)result).Value).Id);
        }
    }
}
