// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using GoLogs.Interfaces;

namespace GoLogs.Services.DeliveryOrder.Api.Models
{
    /// <summary>
    /// Initialize public class Notify .
    /// </summary>
    public class NotifyParty : INotifyParty
    {
        /// <summary>
        /// cargo Owner field.
        /// </summary>
        public int CargoOwnerId { get; set; }

        /// <summary>
        /// Notify field.
        /// </summary>
        public string NotifyAddress { get; set; }

        /// <summary>
        /// Identity field.
        /// </summary>
        [Key]
        public int Id { get; set; }
    }
}
