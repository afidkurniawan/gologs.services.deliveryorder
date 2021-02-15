// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

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
    /// <summary>
    /// Declare public class handler to get DOOrder by DOOrderNumber.
    /// </summary>
    public class Handler : IRequestHandler<Request, DOOrder>
    {
        private readonly IDOOrderContext _context;
        private readonly IProblemCollector _problemCollector;

        /// <summary>
        /// Initializes a new instance of the <see cref="Handler"/> class.
        /// </summary>
        /// <param name="context">Define DOOrderContext.</param>
        /// <param name="problemCollector">Define IProblemCollector.</param>
        public Handler(IDOOrderContext context, IProblemCollector problemCollector)
        {
            _context = context;
            _problemCollector = problemCollector;
        }

        /// <summary>
        /// Handle to get an DOOrder with the specified DOOrderNumber.
        /// </summary>
        /// <param name="request">Specified Request.</param>
        /// <param name="cancellationToken">Specified CancellationToken.</param>
        /// <returns><see cref="DOOrder"/>.</returns>
        public async Task<DOOrder> Handle(Request request, CancellationToken cancellationToken = default)
        {
            Check.NotNull(request, nameof(request));
            return await _context.DOOrders.FirstOrDefaultAsync(new Query().Where(nameof(DOOrder.DOOrderNumber), request.DoNumber), cancellationToken);
        }
    }
}
