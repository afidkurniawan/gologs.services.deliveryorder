﻿// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using GoLogs.Interfaces;

namespace GoLogs.Services.DeliveryOrder.Api.Models
{
    /// <summary>
    /// Initialize public class DONotify .
    /// </summary>
    public class DONotifyParty : IDONotifyParty
    {
        /// <summary>
        /// DOOrderNumber field.
        /// </summary>
        public string DOOrderNumber { get; set; }

        /// <summary>
        /// NotifyAddress field.
        /// </summary>
        public string NotifyAddress { get; set; }

        /// <summary>
        /// Identity field.
        /// </summary>
        [Key]
        public int Id { get; set; }
    }
}
