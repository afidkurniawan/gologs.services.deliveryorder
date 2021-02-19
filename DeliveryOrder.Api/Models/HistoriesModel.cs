// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

using GoLogs.Services.DeliveryOrder.Api.Events;

namespace GoLogs.Services.DeliveryOrder.Api.Models
{
    public class HistoriesModel : History
    {
        public HistoryCreatedEvent HistoryCreatedEvent { get; set; }

        public HistoryConfirmedEvent HistoryConfirmedEvent { get; set; }

        public HistoryWaitingPaymentEvent HistoryWaitingPaymentEvent { get; set; }

        public HistoryPaidEvent HistoryPaidEvent { get; set; }
    }
}
