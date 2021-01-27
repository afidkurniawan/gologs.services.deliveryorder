using GoLogs.Services.DeliveryOrder.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoLogs.Services.DeliveryOrder.Api.Queries.GetList
{
    public class Response
    {
        /// <summary>
        /// Responses for DOOrdersController GetAsync with paging
        /// </summary>
        public IList<DOOrder> DoOrders{ get; set; }

}
}
