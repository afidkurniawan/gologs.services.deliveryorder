// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

namespace GoLogs.Services.DeliveryOrder.Api.Models
{
    /// <summary>
    /// Define public Interface ICreateDOOrder.
    /// </summary>
    public interface ICreateDOOrder
    {
        /// <summary>
        /// Associated CargoOwnerId.
        /// </summary>
        public int CargoOwnerId { get; set; }
    }
}
