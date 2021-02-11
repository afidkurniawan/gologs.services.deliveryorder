// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GoLogs.Services.DeliveryOrder.Api.Application.Internals;
using GoLogs.Services.DeliveryOrder.Api.Models;
using MediatR;
using Nirbito.Framework.Core;
using SqlKata;

namespace GoLogs.Services.DeliveryOrder.Api.Queries.GetByCargoOwnerId
{
    public class Handler : IRequestHandler<Request, IList<DOOrder>>
    {
        private readonly DOOrderContext _context;

        public Handler(DOOrderContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Handle to get an List of DOOrders with the specified CargoOwnerId.
        /// </summary>
        /// <param name="request">Specified CargoOwnerId.</param>
        /// <param name="cancellationToken">Specified cancelation token.</param>
        /// <returns>list of <see cref="DOOrder"/>.</returns>
        public async Task<IList<DOOrder>> Handle(Request request, CancellationToken cancellationToken)
        {
            Check.NotNull(request, nameof(request));
            return await _context.DOOrders.AllAsync(new Query().Where(nameof(DOOrder.CargoOwnerId), request.CargoOwnerId), cancellationToken);
        }
    }
}
