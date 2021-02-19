// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

using System.Collections.Generic;
using GoLogs.Services.DeliveryOrder.Api.Models;
using MediatR;

namespace GoLogs.Services.DeliveryOrder.Api.Queries.GetHistories
{
    /// <summary>
    /// Declare public class request as parameter on controller to get Histories.
    /// </summary>
    public class Request : IRequest<IList<HistoriesModel>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Request"/> class.
        /// </summary>
        /// <param name="page">Define Page.</param>
        /// <param name="pageSize">Define PageSize.</param>
        public Request(int page, int pageSize)
        {
            Page = page;
            PageSize = pageSize;
        }

        /// <summary>
        /// <c>GetAsync Histories </c>associated with the specified Page (int).
        /// </summary>
        public int Page { get; }

        /// <summary>
        /// <c>GetAsync Histories </c>associated with the specified PageSize (int).
        /// </summary>
        public int PageSize { get; }
    }
}
