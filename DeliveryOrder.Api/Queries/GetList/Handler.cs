﻿// -------------------------------------------------------------
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

namespace GoLogs.Services.DeliveryOrder.Api.Queries.GetList
{
    public class Handler : IRequestHandler<Request, IList<DOOrder>>
    {
        private readonly DOOrderContext _context;
        private readonly IProblemCollector _problemCollector;

        public Handler(DOOrderContext context, IProblemCollector problemCollector)
        {
            _context = context;
            _problemCollector = problemCollector;
        }

        /// <summary>
        /// Handle to get an List of DOOrder with the specified Page and PageSize.
        /// </summary>
        /// <param name="request">Specified Page and PageSize.</param>
        /// <param name="cancellationToken">Specifiedcancelation token.</param>
        /// <returns>list of <see cref="DOOrder"/>.</returns>
        public async Task<IList<DOOrder>> Handle(Request request, CancellationToken cancellationToken)
        {
            Check.NotNull(request, nameof(request));
            return await _context.DOOrders.AllAsync(new Query().ForPage(request.Page, request.PageSize), cancellationToken);
        }
    }
}
