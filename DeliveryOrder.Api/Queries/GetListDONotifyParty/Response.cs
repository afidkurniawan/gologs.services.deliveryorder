// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

using System.Collections.Generic;
using GoLogs.Interfaces;
using GoLogs.Services.DeliveryOrder.Api.Models;

namespace GoLogs.Services.DeliveryOrder.Api.Queries.GetListDONotifyParty
{
    /// <summary>
    /// Declare public class response as controler return.
    /// </summary>
    public class Response : IEntity
    {
        /// <summary>
        /// Mandatory property to create DO Notify Party.
        /// </summary>
        public string DOOrderNumber { get; set; }

        /// <summary>
        /// list NotifyAddress is a mandatory Parameter to CREATE DO Notify Party at least 1.
        /// </summary>
        public List<string> NotifyAddress { get; set; }

        /// <summary>
        /// Id.
        /// </summary>
        public int Id { get; set; }
    }
}
