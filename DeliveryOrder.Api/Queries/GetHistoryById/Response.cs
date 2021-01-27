using GoLogs.Services.DeliveryOrder.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoLogs.Services.DeliveryOrder.Api.Queries.GetHistoryById
{
    public class Response
    {
        /// <summary>
        /// Responses for HistoriesController GetAsync by Id
        /// </summary>
        public History History{ get; set; }

}
}
