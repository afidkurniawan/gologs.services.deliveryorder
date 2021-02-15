// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using GoLogs.Interfaces;
using MediatR;

namespace GoLogs.Services.DeliveryOrder.Api.Models
{
    /// <summary>
    /// Initialize public class DOOrder .
    /// </summary>
    public class DOOrder : DOOrderDto, IDOOrder
    {
        /// <summary>
        /// Identity field.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// DOOrderNumber field.
        /// </summary>
        public string DOOrderNumber { get; set; }
    }
}
