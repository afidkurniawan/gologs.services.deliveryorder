using GoLogs.Services.DeliveryOrder.Api.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoLogs.Services.DeliveryOrder.Api.Queries.GetByNumber
{
    public class Request : IRequest<DOOrder>
    {
        public string DoNumber { get; set; }

    }
}
