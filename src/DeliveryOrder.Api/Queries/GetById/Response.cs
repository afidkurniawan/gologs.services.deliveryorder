// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

using GoLogs.Services.DeliveryOrder.Api.Models;

namespace GoLogs.Services.DeliveryOrder.Api.Queries.GetById
{
    /// <summary>
    /// Declare public class response as controler return.
    /// </summary>
    public class Response
    {
        /// <summary>
        /// <c>GetAsync DOOrder </c> DOOrder.
        /// </summary>
        public DOOrder DoOrder { get; set; }
    }
}
