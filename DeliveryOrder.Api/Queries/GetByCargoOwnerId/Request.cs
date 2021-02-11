// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

using System.Collections.Generic;
using GoLogs.Services.DeliveryOrder.Api.Models;
using MediatR;

namespace GoLogs.Services.DeliveryOrder.Api.Queries.GetByCargoOwnerId
{
    /// <summary>
    /// Declare public class request as parameter on controller to get DOOrder GetByCargoOwnerId.
    /// </summary>
    public class Request : IRequest<IList<DOOrder>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Request"/> class.
        /// </summary>
        /// <param name="cargoOwnerId">Define CargoOwnerId.</param>
        public Request(int cargoOwnerId)
        {
            CargoOwnerId = cargoOwnerId;
        }

        /// <summary>
        /// <c>GetAsync DOOrder </c>associated with the specified CargoOwnerId (int).
        /// </summary>
        public int CargoOwnerId { get; }
    }
}
