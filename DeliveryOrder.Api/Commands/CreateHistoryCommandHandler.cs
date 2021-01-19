using GoLogs.Framework.Mvc;
using GoLogs.Services.DeliveryOrder.Api.Application.Internals;
using GoLogs.Services.DeliveryOrder.Api.Models;
using MediatR;
using Nirbito.Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GoLogs.Services.DeliveryOrder.Api.Commands
{

    public class CreateHistoryCommandHandler : IRequestHandler<History, int>
    {
        private DOOrderContext _context;
        private IProblemCollector _problemCollector;

        public CreateHistoryCommandHandler(DOOrderContext context, IProblemCollector problemCollector)
        {
            _context = context;
            _problemCollector = problemCollector;
        }
        public async Task<int> Handle(History request, CancellationToken cancellationToken)
        {
            Check.NotNull(request, nameof(request));
            await _context.Histories.InsertAsync(new History{ DoNumber = request.DoNumber, StateId = request.StateId});
            return await Task.FromResult(1);

        }
    }
}
