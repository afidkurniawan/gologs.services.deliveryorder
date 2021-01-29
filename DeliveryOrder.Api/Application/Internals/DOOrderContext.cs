﻿using GoLogs.Services.DeliveryOrder.Api.Models;
using Microsoft.Extensions.Options;
using Nirbito.Framework.PostgresClient;

namespace GoLogs.Services.DeliveryOrder.Api.Application.Internals
{
    public class DOOrderContext : PgContext
    {
        public PgTable<DOOrder> DOOrders { get; set; }        
        public DOOrderContext(IOptions<PgContextOptions> options) : base(options)
        {
            
        }
    }
}
