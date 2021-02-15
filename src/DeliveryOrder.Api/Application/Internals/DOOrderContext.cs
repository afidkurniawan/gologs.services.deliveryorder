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
    /// <summary>
    /// Class DBContext.
    /// </summary>
    public class DOOrderContext : PgContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DOOrderContext"/> class.
        /// </summary>
        /// <param name="options">Define PgContextOptions.</param>
        public DOOrderContext(IOptions<PgContextOptions> options)
            : base(options)
        {
        }

        /// <summary>
        /// Declare Table.
        /// </summary>
        public PgTable<DOOrder> DOOrders { get; set; }
    }
}
