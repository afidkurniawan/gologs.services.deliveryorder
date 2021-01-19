using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GoLogs.Framework.Mvc;
using GoLogs.Services.DeliveryOrder.Api.Application.Internals;
using GoLogs.Services.DeliveryOrder.Api.Models;
using MediatR;
using Nirbito.Framework.Core;
using SqlKata;

namespace GoLogs.Services.DeliveryOrder.Api.Queries.GetHistoryList
{
    public class Handler : IRequestHandler<Request, IList<History>>
    {
        private DOOrderContext _context;
        private IProblemCollector _problemCollector;
        public Handler(DOOrderContext context, IProblemCollector problemCollector)
        {
            _context = context;
            _problemCollector = problemCollector;
        }
        public async Task<IList<History>> Handle(Request request, CancellationToken cancellationToken)
        {
            Check.NotNull(request, nameof(request));
            return await _context.Histories.AllAsync(new Query().ForPage(request.Page,request.PageSize));
        }
        
    }
}
