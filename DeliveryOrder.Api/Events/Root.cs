// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoLogs.Services.DeliveryOrder.Api.Events
{
    public class Root
    {
        public HistoryCreatedEvent HistoryCreatedEvent { get; set; }

        public HistoryConfirmedEvent HistoryConfirmedEvent { get; set; }

        public HistoryWaitingPaymentEvent HistoryWaitingPaymentEvent { get; set; }

        public HistoryPaidEvent HistoryPaidEvent { get; set; }
    }
}
