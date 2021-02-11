// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

using GoLogs.Services.DeliveryOrder.Api.Models;
using MediatR;

namespace GoLogs.Services.DeliveryOrder.Api.Queries.GetById
{
    public class Request : IRequest<DOOrder>
    {
        public Request(int id)
        {
            Id = id;
        }

        /// <summary>
        /// <c>GetAsync DOOrder </c>associated with the specified ID (int).
        /// </summary>
        public int Id { get; }
    }
}
