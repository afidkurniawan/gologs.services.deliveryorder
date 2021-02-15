// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

using GoLogs.Services.DeliveryOrder.Api.Models;
using MediatR;

namespace GoLogs.Services.DeliveryOrder.Api.Queries.GetById
{
    /// <summary>
    /// Declare public class request as parameter on controller to get DOOrder GetById.
    /// </summary>
    public class Request : IRequest<DOOrder>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Request"/> class.
        /// </summary>
        /// <param name="id">Define Id.</param>
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
