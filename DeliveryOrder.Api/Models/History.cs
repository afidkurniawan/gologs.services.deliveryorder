// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using GoLogs.Interfaces;

namespace GoLogs.Services.DeliveryOrder.Api.Models
{
    public class History : IHistory
    {
        /// <summary>
        /// DOOrderNumber is a mandatory Parameter to CREATE History.
        /// </summary>
        public string DOOrderNumber { get; set; }

        /// <summary>
        /// Currentstate field.
        /// </summary>
        public string CurrentState { get; set; }

        /// <summary>
        /// EventStore fill with List of event.
        /// </summary>
        public string EventStore { get; set; }

        /// <summary>
        /// Id.
        /// </summary>
        [Key]
        public int Id { get; set; }
    }
}
