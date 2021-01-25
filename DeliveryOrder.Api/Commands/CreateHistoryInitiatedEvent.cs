using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoLogs.Services.DeliveryOrder.Api.Commands
{
    public class CreateHistoryInitiatedEvent
    {
        public string DOOrderNumber { get; set; }
        public int StateId { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
