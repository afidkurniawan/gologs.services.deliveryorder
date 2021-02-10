// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using GoLogs.Interfaces;

namespace GoLogs.Services.DeliveryOrder.Api.Models
{
    public class DOOrder : DOOrderDto, IDOOrder
    {
        [Key]
        public int Id { get; set; }

        public string DOOrderNumber { get; set; }
    }
}
