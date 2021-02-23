// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoLogs.Framework.Mvc;
using GoLogs.Services.DeliveryOrder.Api.Application.Internals;
using GoLogs.Services.DeliveryOrder.Api.Models;
using MediatR;
using Nirbito.Framework.Core;
using SqlKata;

namespace GoLogs.Services.DeliveryOrder.Api.Queries.GetListDONotifyParty
{
    /// <summary>
    /// Declare public class handler to get DOOrder by Id.
    /// </summary>
    public class Handler : IRequestHandler<Request, List<Response>>
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
        public async Task<List<Response>> Handle(Request request, CancellationToken cancellationToken)
        {
            Check.NotNull(request, nameof(request));
            var notifyParties = await _context.DONotifyParties.AllAsync(new Query().ForPage(request.Page, request.PageSize), cancellationToken);
            var responses = new List<Response>();
            foreach (var notifyparty in notifyParties)
            {
                responses.Add(new Response { Id = notifyparty.Id,  DOOrderNumber = notifyparty.DOOrderNumber, NotifyAddress = notifyparty.NotifyAddress.Split(';').ToList() });
            }

            return responses;
        }
    }
}
