// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

namespace GoLogs.Services.DeliveryOrder.Api.Models
{
    /// <summary>
    /// Initialize public interface IEntity as Base Entity.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Mandatory Field for each entity.
        /// </summary>
        int Id { get; set; }
    }
}
