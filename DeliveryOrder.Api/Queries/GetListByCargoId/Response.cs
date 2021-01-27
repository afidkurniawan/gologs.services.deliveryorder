using GoLogs.Services.DeliveryOrder.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoLogs.Services.DeliveryOrder.Api.Queries.GetListByCargoId
{
    public class Response
    {
        /// <summary>
        /// Responses for DOOrdersController GetAsync CargoOwnerId and paging
        /// </summary>
        public IList<DOOrder> DoOrders{ get; set; }

}
}
