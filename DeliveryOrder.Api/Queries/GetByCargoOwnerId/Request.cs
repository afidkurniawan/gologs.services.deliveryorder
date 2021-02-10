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
    public class Request : IRequest<IList<DOOrder>>
    {
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
