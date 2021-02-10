// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

namespace GoLogs.Services.DeliveryOrder.Api.Models
{
    public interface ICreateDOOrder : IEntity
    {
        /// <summary>
        /// Mandatory Parameter to create DOOrderNumber.
        /// </summary>
        public int CargoOwnerId { get; }
    }
}
