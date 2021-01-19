using System.Threading;
using System.Threading.Tasks;
using GoLogs.Framework.Mvc;
using GoLogs.Services.DeliveryOrder.Api.Application.Internals;
using GoLogs.Services.DeliveryOrder.Api.Models;
using MediatR;
using Nirbito.Framework.Core;
using SqlKata;

namespace GoLogs.Services.DeliveryOrder.Api.Queries.GetHistoryById
{
    public class Handler : IRequestHandler<Request, History>
    {
        private DOOrderContext _context;
        private IProblemCollector _problemCollector;
        public Handler(DOOrderContext context, IProblemCollector problemCollector)
        {
            _context = context;
            _problemCollector = problemCollector;
        }
        public async Task<History> Handle(Request request, CancellationToken cancellationToken)
        {
            Check.NotNull(request, nameof(request));
            return await _context.Histories.FirstOrDefaultAsync(new Query().Where(nameof(History.Id), request.Id));
        }
        
    }
}
