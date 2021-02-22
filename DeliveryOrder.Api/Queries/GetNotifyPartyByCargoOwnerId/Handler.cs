// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoLogs.Framework.Mvc;
using GoLogs.Services.DeliveryOrder.Api.Application.Internals;
using GoLogs.Services.DeliveryOrder.Api.Models;
using MediatR;
using Nirbito.Framework.Core;
using SqlKata;

namespace GoLogs.Services.DeliveryOrder.Api.Queries.GetNotifyPartyByCargoOwnerId
{
    /// <summary>
    /// Declare public class handler to get DOOrder by Id.
    /// </summary>
    public class Handler : IRequestHandler<Request, Response>
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
        /// Handle to get an NotifyParty with the specified Id.
        /// </summary>
        /// <param name="request">Specified Request.</param>
        /// <param name="cancellationToken">Specified CancellationToken.</param>
        /// <returns><see cref="NotifyParty"/>.</returns>
        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            Check.NotNull(request, nameof(request));
            var notifyParties = await _context.NotifyParties.FirstOrDefaultAsync(new Query().Where(nameof(NotifyParty.CargoOwnerId), request.CargoOwnerId), cancellationToken);
            return notifyParties == null ? null : new Response { Id = notifyParties.Id, CargoOwnerId = notifyParties.CargoOwnerId, NotifyAddress = notifyParties.NotifyAddress.Split(';').ToList() };
        }
    }
}
