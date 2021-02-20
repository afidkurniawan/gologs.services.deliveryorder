// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

using System.Collections.Generic;
using MediatR;

namespace GoLogs.Services.DeliveryOrder.Api.Models
{
    /// <summary>
    /// Initialize DONotifyDto class.
    /// </summary>
    public class DONotifyPartyDto : IRequest<string>
    {
        /// <summary>
        /// DOOrderNumber is a mandatory Parameter to CREATE DONotify.
        /// </summary>
        public string DOOrderNumber { get; set; }

        /// <summary>
        /// list NotifyAddress is a mandatory Parameter to CREATE DONotify at least 1.
        /// </summary>
        public List<string> NotifyAddress { get; set; }
    }
}
