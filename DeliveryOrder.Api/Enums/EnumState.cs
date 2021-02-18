// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoLogs.Services.DeliveryOrder.Api.Enums
{
    /// <summary>
    /// Delivery Order document state.
    /// </summary>
    public enum EnumState
    {
        /// <summary>
        /// Created DO
        /// </summary>
        Created = 1,

        /// <summary>
        /// Confirmed DO by SL.
        /// </summary>
        Confirmed = 2,

        /// <summary>
        /// Waiting PI payment
        /// </summary>
        WaitingPayment = 3,

        /// <summary>
        /// PI has been paid
        /// </summary>
        Paid = 4,
    }
}
