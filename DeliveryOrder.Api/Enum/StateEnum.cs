using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoLogs.Services.DeliveryOrder.Api.Enum
{
    public enum StateEnum
    {        
        Created=1,
        Confirmed=2,
        WaitingPayment=3,
        Paid=4,
         
    }
}
