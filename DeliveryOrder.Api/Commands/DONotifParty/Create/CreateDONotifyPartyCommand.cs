// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

using System.Collections.Generic;
using MediatR;

namespace GoLogs.Services.DeliveryOrder.Api.Commands.DONotifParty.Create
{
    public class CreateDONotifyPartyCommand : IRequest<CreateDONotifyPartyResponse>
    {
        /// <summary>
        /// Mandatory property to create NotifyParty.
        /// </summary>
        public string DOOrderNumber { get; set; }

        /// <summary>
        /// list NotifyAddress is a mandatory Parameter to CREATE NotifyParty at least 1.
        /// </summary>
        public List<string> NotifyAddress { get; set; }
    }
}
