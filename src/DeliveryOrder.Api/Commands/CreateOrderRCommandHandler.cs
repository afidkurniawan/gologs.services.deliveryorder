using GoLogs.Framework.Mvc;
using GoLogs.Services.DeliveryOrder.Api.Application.Internals;
using GoLogs.Services.DeliveryOrder.Api.Models;
using MediatR;
using Nirbito.Framework.Core;
using SqlKata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GoLogs.Services.DeliveryOrder.Api.Commands
{
    public class CreateOrderRCommandHandler : IRequestHandler<DOOrder, int>
    {
        private DOOrderReadContext _context;
        private IProblemCollector _problemCollector;

        public CreateOrderRCommandHandler(DOOrderReadContext context, IProblemCollector problemCollector)
        {
            _context = context;
            _problemCollector = problemCollector;
        }

        public async Task<int> Handle(DOOrder request, CancellationToken cancellationToken)
        {
            Check.NotNull(request, nameof(request));
            var cargoOwnerId = request.CargoOwnerId;
            var doOrders = await _context.DOOrders.AllAsync(new Query().Where(nameof(request.CargoOwnerId), cargoOwnerId));
            int lastDoOrdersId = doOrders.Count + 1;
            var doNumber = "DO" + lastDoOrdersId;
            request.DoOrderNumber = doNumber;
          //  await _context.DOOrders.InsertAsync(request);
            return await Task.FromResult(1);
        }
    }
}
