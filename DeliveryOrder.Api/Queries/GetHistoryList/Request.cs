using GoLogs.Services.DeliveryOrder.Api.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoLogs.Services.DeliveryOrder.Api.Queries.GetHistoryList
{
    public class Request : IRequest<IList<History>>
    {
        /// <summary>
        /// Get List of Histories with paging
        /// Page : the page number of history data you want to display
        /// PageSize : the number of rows of history data on each page that you want to display
        /// </summary>
        public int Page { get; set; }
        public int PageSize { get; set; }

    }
}
