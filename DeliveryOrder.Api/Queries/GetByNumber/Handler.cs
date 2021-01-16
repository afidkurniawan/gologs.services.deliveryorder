using System.Threading;
using System.Threading.Tasks;
using GoLogs.Framework.Mvc;
using GoLogs.Services.DeliveryOrder.Api.Application.Internals;
using GoLogs.Services.DeliveryOrder.Api.Models;
using MediatR;
using Nirbito.Framework.Core;
using SqlKata;

namespace GoLogs.Services.DeliveryOrder.Api.Queries.GetByNumber
{
    public class Handler : IRequestHandler<Request, DOOrder>
    {
        private DOOrderContext _context;
        private IProblemCollector _problemCollector;
        public Handler(DOOrderContext context, IProblemCollector problemCollector)
        {
            _context = context;
            _problemCollector = problemCollector;
        }
        public async Task<DOOrder> Handle(Request request, CancellationToken cancellationToken)
        {
            Check.NotNull(request, nameof(request));
            return await _context.Doorders.FirstOrDefaultAsync(new Query().Where(nameof(DOOrder.DoOrderNumber), request.DoNumber));
        }
        
    }
}
