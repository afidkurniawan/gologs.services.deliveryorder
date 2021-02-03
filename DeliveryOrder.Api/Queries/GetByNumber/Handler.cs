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
        private readonly DOOrderContext _context;
        private readonly IProblemCollector _problemCollector;
        public Handler(DOOrderContext context, IProblemCollector problemCollector)
        {
            _context = context;
            _problemCollector = problemCollector;
        }
        /// <summary>
        /// Handle to get an DOOrder associated with the specified DOOrderNumer
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>DOOrder</returns>
        public async Task<DOOrder> Handle(Request request, CancellationToken cancellationToken)
        {
            Check.NotNull(request, nameof(request));
            return await _context.DOOrders.FirstOrDefaultAsync(new Query().Where(nameof(DOOrder.DOOrderNumber), request.DoNumber), cancellationToken);
        }
        
    }
}
