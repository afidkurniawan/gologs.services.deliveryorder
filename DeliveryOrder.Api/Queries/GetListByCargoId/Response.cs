using GoLogs.Services.DeliveryOrder.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoLogs.Services.DeliveryOrder.Api.Queries.GetListByCargoId
{
    public class Response
    {
        public IList<DOOrder> DoOrders{ get; set; }

}
}
