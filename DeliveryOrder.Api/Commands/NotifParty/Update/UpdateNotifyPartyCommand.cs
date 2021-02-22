﻿// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

using System.Collections.Generic;
using MediatR;

namespace GoLogs.Services.DeliveryOrder.Api.Commands.NotifParty.Update
{
    public class UpdateNotifyPartyCommand : IRequest<UpdateNotifyPartyResponse>
    {
        /// <summary>
        /// Mandatory property to create doorder number.
        /// </summary>
        public int CargoOwnerId { get; set; }

        /// <summary>
        /// list NotifyAddress is a mandatory Parameter to CREATE Notify at least 1.
        /// </summary>
        public List<string> NotifyAddress { get; set; }
    }
}
