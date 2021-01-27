using GoLogs.Services.DeliveryOrder.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoLogs.Services.DeliveryOrder.Api.Queries.GetById
{
    public class Response
    {
        /// <summary>
        /// Responses for DOOrdersController GetAsync by Id
        /// </summary>
        public DOOrder DoOrder{ get; set; }

}
}
