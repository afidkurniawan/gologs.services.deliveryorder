// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

using System.Collections.Generic;
using GoLogs.Services.DeliveryOrder.Api.Models;

namespace GoLogs.Services.DeliveryOrder.Api.Queries.GetHistoriesByDOOrderNumber
{
    /// <summary>
    ///  Declare public class response as controler return.
    /// </summary>
    public class Response
    {
        /// <summary>
        /// <c>GetAsync Histories </c> Histories.
        /// </summary>
        public IList<HistoriesModel> Histories { get; set; }
    }
}
