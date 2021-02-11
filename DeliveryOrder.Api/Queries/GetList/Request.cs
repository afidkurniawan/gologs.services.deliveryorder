// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

using System.Collections.Generic;
using GoLogs.Services.DeliveryOrder.Api.Models;
using MediatR;

namespace GoLogs.Services.DeliveryOrder.Api.Queries.GetList
{
    public class Request : IRequest<IList<DOOrder>>
    {
        public Request(int page, int pageSize)
        {
            Page = page;
            PageSize = pageSize;
        }

        /// <summary>
        /// <c>GetAsync DOOrders </c>associated with the specified Page (int).
        /// </summary>
        public int Page { get; }

        /// <summary>
        /// <c>GetAsync DOOrders </c>associated with the specified PageSize (int).
        /// </summary>
        public int PageSize { get; }
    }
}
