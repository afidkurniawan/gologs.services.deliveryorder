
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
        public Request(int id)
        {
            Id = id;
        }

        /// <summary>
        /// <c>Get DOOrder </c>associated with the specified ID (int)
        /// </summary>
        public int Id { get; }

    }
}
