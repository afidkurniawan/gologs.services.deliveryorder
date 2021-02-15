// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------
using GoLogs.Services.DeliveryOrder.Api.Models;
using MediatR;

// ReSharper disable InconsistentNaming
namespace GoLogs.Services.DeliveryOrder.Api.Commands
{
    /// <summary>
    /// Public class request for Create DONumber.
    /// </summary>
    public class CreateOrderCommand : IRequest<DOOrder>
    {
        /// <summary>
        /// Mandatory property to create doorder number.
        /// </summary>
        public int CargoOwnerId { get; set; }
    }
}
