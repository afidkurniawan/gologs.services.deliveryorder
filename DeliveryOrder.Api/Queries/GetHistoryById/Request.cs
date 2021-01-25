using GoLogs.Services.DeliveryOrder.Api.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoLogs.Services.DeliveryOrder.Api.Queries.GetHistoryById
{
    public class Request : IRequest<History>
    {
        /// <summary>
        /// The ID of Hitory data you wanto to find
        /// </summary>
        public int Id { get; set; }

    }
}
