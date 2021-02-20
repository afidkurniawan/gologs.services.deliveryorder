// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using GoLogs.Services.DeliveryOrder.Api.Application.Internals;
using GoLogs.Services.DeliveryOrder.Api.Commands;
using GoLogs.Services.DeliveryOrder.Api.Commands.HistoryCreated;
using GoLogs.Services.DeliveryOrder.Api.Events;
using Newtonsoft.Json;
using SqlKata;

namespace GoLogs.Services.DeliveryOrder.Api.Repository
{
    /// <summary>
    /// History rpository.
    /// </summary>
    public class HistoryRepository
    {
        private readonly DOOrderContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="HistoryRepository"/> class.
        /// </summary>
        /// <param name="context">Define DBContext.</param>
        public HistoryRepository(DOOrderContext context)
        {
            _context = context;
        }

        public HistoryRepository()
        {
        }

            /// <summary>
            /// task to get events.
            /// </summary>
            /// <param name="dONumber">Defined DO Number.</param>
            /// <returns>The <see cref="CreateHistoryOrderCommand"/>.</returns>
        public async Task<CreateHistoryOrderCommand> Get(string dONumber)
        {
            var historyOrderCommand = new CreateHistoryOrderCommand(dONumber);
            var histories = await _context.Histories.AllAsync(new Query().Where("DOOrderNumber", dONumber));
            foreach (var evnts in histories)
            {
                var jsonEvent = JsonConvert.DeserializeObject<List<Root>>("[" + evnts.EventStore + "]");
                foreach (var evnt in jsonEvent)
                {
                    if (evnt.HistoryCreatedEvent != null)
                    {
                        historyOrderCommand.ApplyEvent(evnt.HistoryCreatedEvent);
                    }

                    if (evnt.HistoryConfirmedEvent != null)
                    {
                        historyOrderCommand.ApplyEvent(evnt.HistoryConfirmedEvent);
                    }

                    if (evnt.HistoryWaitingPaymentEvent != null)
                    {
                        historyOrderCommand.ApplyEvent(evnt.HistoryWaitingPaymentEvent);
                    }

                    if (evnt.HistoryPaidEvent != null)
                    {
                        historyOrderCommand.ApplyEvent(evnt.HistoryPaidEvent);
                    }
                }
            }

            return historyOrderCommand;
        }
    }
}
