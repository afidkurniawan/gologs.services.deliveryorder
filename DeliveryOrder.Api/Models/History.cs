// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using GoLogs.Interfaces;

namespace GoLogs.Services.DeliveryOrder.Api.Models
{
    public class History : HistoryDto, IHistory
    {
        public string CurrentState { get; set; }

        public string EventStore { get; set; }

        [Key]
        public int Id { get; set; }
    }
}
