// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GoLogs.Framework.Mvc;
using GoLogs.Services.DeliveryOrder.Api.Application.Internals;
using GoLogs.Services.DeliveryOrder.Api.Models;
using MediatR;
using Nirbito.Framework.Core;
using SqlKata;

namespace GoLogs.Services.DeliveryOrder.Api.Queries.GetListByCargoOwnerId
{
    /// <summary>
    /// Declare public class handler to get DOOrder by CargoOwnerId.
    /// </summary>
    public class Handler : IRequestHandler<Request, IList<DOOrder>>
    {
        private readonly DOOrderContext _context;
        private readonly IProblemCollector _problemCollector;

        /// <summary>
        /// Initializes a new instance of the <see cref="Handler"/> class.
        /// </summary>
        /// <param name="context">Define DOOrderContext.</param>
        /// <param name="problemCollector">Define IProblemCollector.</param>
        public Handler(DOOrderContext context, IProblemCollector problemCollector)
        {
            _context = context;
            _problemCollector = problemCollector;
        }

        /// <summary>
        /// Handle to get an List of DOOrders with the specified CargoOwnerId, Page and PageSize.
        /// </summary>
        /// <param name="request">Specified Request.</param>
        /// <param name="cancellationToken">Specified CancellationToken.</param>
        /// <returns>list of <see cref="DOOrder"/>.</returns>
        public async Task<IList<DOOrder>> Handle(Request request, CancellationToken cancellationToken)
        {
            Check.NotNull(request, nameof(request));
            return await _context.DOOrders.AllAsync(new Query().Where(nameof(DOOrder.CargoOwnerId), request.CargoOwnerId).ForPage(request.Page, request.PageSize), cancellationToken);
        }
    }
}
