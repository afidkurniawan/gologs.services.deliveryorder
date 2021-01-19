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
        
        public int Page { get; set; }
        public int PageSize { get; set; }

    }
}
