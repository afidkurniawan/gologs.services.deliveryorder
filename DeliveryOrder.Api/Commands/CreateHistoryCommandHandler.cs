using Automatonymous;
using GoLogs.Framework.Mvc;
using GoLogs.Services.DeliveryOrder.Api.Application.Internals;
using GoLogs.Services.DeliveryOrder.Api.Enum;
using GoLogs.Services.DeliveryOrder.Api.Models;
using MassTransit;
using MediatR;
using Nirbito.Framework.Core;
using SqlKata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using State = GoLogs.Services.DeliveryOrder.Api.Models.State;

namespace GoLogs.Services.DeliveryOrder.Api.Commands
{

    public class CreateHistoryCommandHandler : IRequestHandler<History, int>
    {
        private DOOrderContext _context;
        private IProblemCollector _problemCollector;
        private readonly IBus _bus;
        public CreateHistoryCommandHandler(DOOrderContext context, IProblemCollector problemCollector,IBus bus)
        {
            _context = context;
            _problemCollector = problemCollector;
            _bus = bus;
        }
        public async Task<int> Handle(History request, CancellationToken cancellationToken)
        {
            Check.NotNull(request, nameof(request));
            var doorder = await _context.DOOrders.FirstOrDefaultAsync(new Query().Where(nameof(DOOrder.DoOrderNumber), request.DOOrderNumber));
            if (doorder != null) {
                request.StateId = (int)StateEnum.Created;
                await _context.Histories.InsertAsync(request);

                var createHistoryInitiatedEvent = new CreateHistoryInitiatedEvent 
                { 
                    DOOrderNumber = request.DOOrderNumber,
                    StateId = (int)StateEnum.Created
                };
                await _bus.Publish(createHistoryInitiatedEvent);                
                return await Task.FromResult(1);
            }
            return await Task.FromResult(0);
        }     
    }
}
