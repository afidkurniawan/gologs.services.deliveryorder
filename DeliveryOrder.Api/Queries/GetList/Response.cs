// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

using System.Collections.Generic;
using GoLogs.Services.DeliveryOrder.Api.Models;

namespace GoLogs.Services.DeliveryOrder.Api.Queries.GetList
{
    public class Response
    {
        /// <summary>
        /// <c>GetAsync DOOrders </c> DOOrders.
        /// </summary>
        public IList<DOOrder> DoOrders { get; set; }
    }
}
