// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GoLogs.Framework.Mvc;
using GoLogs.Services.DeliveryOrder.Api.Application.Internals;
using GoLogs.Services.DeliveryOrder.Api.Events;
using GoLogs.Services.DeliveryOrder.Api.Models;
using MediatR;
using Newtonsoft.Json;
using Nirbito.Framework.Core;
using SqlKata;

namespace GoLogs.Services.DeliveryOrder.Api.Queries.GetHistories
{
    public class Handler : IRequestHandler<Request, IList<HistoriesModel>>
    {
        private readonly DOOrderContext _context;
        private readonly IProblemCollector _problemCollector;

        /// <summary>
        /// Initializes a new instance of the <see cref="Handler"/> class.
        /// </summary>
        /// <param name="context">Define DOOrderContext.</param>
        /// <param name="problemCollector">Define IProblemCollector.</param>
        public Handler(DOOrderContext context, IProblemCollector problemCollector)
        {
            _context = context;
            _problemCollector = problemCollector;
        }

        /// <summary>
        /// Handle to get an List of History with the specified Page and PageSize.
        /// </summary>
        /// <param name="request">Specified Request.</param>
        /// <param name="cancellationToken">Specified CancellationToken.</param>
        /// <returns>list of <see cref="HistoriesModel"/>.</returns>
        public async Task<IList<HistoriesModel>> Handle(Request request, CancellationToken cancellationToken)
        {
            var histories = await _context.Histories.AllAsync(new Query().ForPage(request.Page, request.PageSize), cancellationToken);
            var historiesModel = new List<HistoriesModel>();
            foreach (var history in histories)
            {
                var events = JsonConvert.DeserializeObject<List<HistoriesModel>>("[" + history.EventStore + "]");
                var createdEvent = new HistoryCreatedEvent();
                var confirmedEvent = new HistoryConfirmedEvent();
                var waitingEvent = new HistoryWaitingPaymentEvent();
                var paidEvent = new HistoryPaidEvent();

                foreach (var evnt in events)
                {
                    if (evnt.HistoryCreatedEvent != null)
                    {
                        createdEvent = evnt.HistoryCreatedEvent;
                    }

                    if (evnt.HistoryConfirmedEvent != null)
                    {
                        confirmedEvent = evnt.HistoryConfirmedEvent;
                    }

                    if (evnt.HistoryWaitingPaymentEvent != null)
                    {
                        waitingEvent = evnt.HistoryWaitingPaymentEvent;
                    }

                    if (evnt.HistoryPaidEvent != null)
                    {
                        paidEvent = evnt.HistoryPaidEvent;
                    }
                }

                historiesModel.Add(new HistoriesModel
                {
                    Id = history.Id,
                    DOOrderNumber = history.DOOrderNumber,
                    CurrentState = history.CurrentState,
                    EventStore = history.EventStore,
                    HistoryCreatedEvent = createdEvent,
                    HistoryConfirmedEvent = confirmedEvent,
                    HistoryWaitingPaymentEvent = waitingEvent,
                    HistoryPaidEvent = paidEvent
                });
            }

            return historiesModel;
        }
    }
}
