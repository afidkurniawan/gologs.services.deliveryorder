using GoLogs.Services.DeliveryOrder.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoLogs.Services.DeliveryOrder.Api.Queries.GetHistoryList
{
    public class Response
    {
        /// <summary>
        /// Responses for HistoriesController  GetAsync
        /// </summary>
        public IList<History> Histories{ get; set; }

}
}
