using GoLogs.Services.DeliveryOrder.Api.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoLogs.Services.DeliveryOrder.Api.Queries.GetById
{
    public class Request : IRequest<DOOrder>
    {
        /// <summary>
        /// The ID of DOOrder data you wanto to find
        /// </summary>
        public int Id { get; set; }

    }
}
