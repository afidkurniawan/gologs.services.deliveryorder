// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

using System.Collections.Generic;
using GoLogs.Services.DeliveryOrder.Api.Models;
using MediatR;

namespace GoLogs.Services.DeliveryOrder.Api.Commands.NofityParty.Create
{
    public class CreateNotifyPartyCommand : IRequest<CreateNotifyPartyResponse>
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
