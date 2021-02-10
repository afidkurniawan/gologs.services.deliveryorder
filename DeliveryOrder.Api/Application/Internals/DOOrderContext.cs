// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

using GoLogs.Services.DeliveryOrder.Api.Models;
using Microsoft.Extensions.Options;
using Nirbito.Framework.PostgresClient;

namespace GoLogs.Services.DeliveryOrder.Api.Application.Internals
{
    public class DOOrderContext : PgContext
    {
        public DOOrderContext(IOptions<PgContextOptions> options)
            : base(options)
        {
        }

        public PgTable<DOOrder> DOOrders { get; set; }
    }
}
