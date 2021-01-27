using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoLogs.Services.DeliveryOrder.Api.Enum
{
    public enum StateEnum
    {        
        /// <summary>
        /// List of Enum to create DOOrder State must be equat with State Table
        /// </summary>
        Created=1,
        Confirmed=2,
        WaitingPayment=3,
        Paid=4,
         
    }
}
