// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

using GoLogs.Services.DeliveryOrder.Api.Models;
using MediatR;

namespace GoLogs.Services.DeliveryOrder.Api.Queries.GetNotifyPartyByCargoOwnerId
{
    /// <summary>
    /// Declare public class request as parameter on controller to get DOOrder GetById.
    /// </summary>
    public class Request : IRequest<Response>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Request"/> class.
        /// </summary>
        /// <param name="cargoOwnerId">Define Id.</param>
        public Request(int cargoOwnerId)
        {
            CargoOwnerId = cargoOwnerId;
        }

        /// <summary>
        /// <c>GetAsync NotifyParty </c>associated with the specified CargoOwnerId (int).
        /// </summary>
        public int CargoOwnerId { get; }
    }
}
