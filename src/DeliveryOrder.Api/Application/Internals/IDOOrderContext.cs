// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

using GoLogs.Services.DeliveryOrder.Api.Models;
using Nirbito.Framework.PostgresClient;

namespace GoLogs.Services.DeliveryOrder.Api.Application.Internals
{
    public interface IDOOrderContext
    {
        /// <summary>
        /// Declare Table.
        /// </summary>
        PgTable<DOOrder> DOOrders { get; set; }
    }
}
