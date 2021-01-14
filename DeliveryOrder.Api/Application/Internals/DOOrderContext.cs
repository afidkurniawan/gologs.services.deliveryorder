using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoLogs.Services.DeliveryOrder.Api.Models;
using Microsoft.Extensions.Options;
using PostgresClient;
namespace GoLogs.Services.DeliveryOrder.Api.Application.Internals
{
    public class DOOrderContext : PgContext
    {
        public PgTable<DOOrder> Doorders { get; set; }
        public DOOrderContext(IOptions<PgContextOptions> options) : base(options)
        {
        }
    }
}
