using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoLogs.Services.DeliveryOrder.Api.Enum
{
    public enum StateEnum
    {
        Initial,
        Created,
        Confirmed,
        WaitingPayment,
        Paid,
         
    }
}
