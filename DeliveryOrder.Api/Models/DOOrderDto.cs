// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

using MediatR;

namespace GoLogs.Services.DeliveryOrder.Api.Models
{
    public class DOOrderDto : IRequest<int>
    {
        /// <summary>
        /// CargoOwnerId is a mandatory Parameter to CREATE DOOrderNumber.
        /// </summary>
        public int CargoOwnerId { get; set; }
    }
}
