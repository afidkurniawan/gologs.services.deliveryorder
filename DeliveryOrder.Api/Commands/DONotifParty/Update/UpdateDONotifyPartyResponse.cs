﻿// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

using System.Collections.Generic;
using GoLogs.Interfaces;

namespace GoLogs.Services.DeliveryOrder.Api.Commands.DONotifParty.Update
{
    public class UpdateDONotifyPartyResponse : IEntity
    {
        /// <summary>
        /// Mandatory property to create NotifyParty.
        /// </summary>
        public string DOOrderNumber { get; set; }

        /// <summary>
        /// list NotifyAddress is a mandatory Parameter to CREATE Notify at least 1.
        /// </summary>
        public List<string> NotifyAddress { get; set; }

        /// <summary>
        /// Id.
        /// </summary>
        public int Id { get; set; }
    }
}
