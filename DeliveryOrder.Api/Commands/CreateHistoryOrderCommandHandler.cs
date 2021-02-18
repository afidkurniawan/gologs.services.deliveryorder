// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using GoLogs.Framework.Mvc;
using GoLogs.Services.DeliveryOrder.Api.Application.Internals;
using GoLogs.Services.DeliveryOrder.Api.Enums;
using GoLogs.Services.DeliveryOrder.Api.Events;
using GoLogs.Services.DeliveryOrder.Api.Models;
using GoLogs.Services.DeliveryOrder.Api.Repository;
using MediatR;
using Newtonsoft.Json;
using SqlKata;

namespace GoLogs.Services.DeliveryOrder.Api.Commands
{
    public class CreateHistoryOrderCommandHandler : IRequestHandler<CreateHistoryOrderCommand, History>
    {
        private readonly DOOrderContext _context;
        private readonly IProblemCollector _problemCollector;

        public CreateHistoryOrderCommandHandler(DOOrderContext context, IProblemCollector problemCollector)
        {
            _context = context;
            _problemCollector = problemCollector;
        }

        /// <summary>
        /// Handle to Create History.
        /// </summary>
        /// <param name="request">Define Request.</param>
        /// <param name="cancellationToken">Define CancelationToken.</param>
        /// <returns>The <see cref="History"/>.</returns>
        public async Task<History> Handle(CreateHistoryOrderCommand request, CancellationToken cancellationToken)
        {
            var historyRepository = new HistoryRepository(_context);

            var history = await historyRepository.Get(request.DOOrderNumber);
            history.OnCreated();
            return await Save(history, cancellationToken);
        }

        private async Task<History> Save(CreateHistoryOrderCommand request, CancellationToken cancellationToken)
        {
            var history = new History();
            var transactionOptions = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted,
                Timeout = TransactionManager.MaximumTimeout
            };
            using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew, transactionOptions, TransactionScopeAsyncFlowOption.Enabled))
            {
                var isDOExist = await _context.DOOrders.FirstOrDefaultAsync(new Query().Where("DOOrderNumber", request.DOOrderNumber), cancellationToken);
                if (isDOExist == null)
                {
                    return null;
                }

                history = await _context.Histories.FirstOrDefaultAsync(new Query().Where("DOOrderNumber", request.DOOrderNumber), cancellationToken);
                if (history != null)
                {
                    return new History();
                }

                var newEvents = request.GetUncommitedEvents();
                var jsonEventStore = String.Empty;
                foreach (var evnt in newEvents)
                {
                    dynamic result = new ExpandoObject();
                    switch (evnt)
                    {
                        case HistoryCreatedEvent historyCreatedEvent:
                            result.HistoryCreatedEvent = historyCreatedEvent;
                            jsonEventStore += JsonConvert.SerializeObject(result, Formatting.Indented);
                            history = new History { DOOrderNumber = request.DOOrderNumber, CurrentState = Enum.GetName(typeof(EnumState), EnumState.Created), EventStore = jsonEventStore };
                            await _context.Histories.InsertAsync(history, cancellationToken);
                            break;
                    }
                }

                request.EventsCommitted();
            }

            return history;
        }
    }
}
