// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

using System;
using System.Collections.Generic;
using GoLogs.Contracts.Events;
using GoLogs.Services.DeliveryOrder.Api.Enums;
using GoLogs.Services.DeliveryOrder.Api.Events;
using GoLogs.Services.DeliveryOrder.Api.Models;
using MediatR;

namespace GoLogs.Services.DeliveryOrder.Api.Commands.HistoryCreated
{
    public class CreateHistoryOrderCommand : IRequest<History>
    {
        private readonly IList<IHistoryEvent> _allEvents = new List<IHistoryEvent>();
        private readonly IList<IHistoryEvent> _unCommitedEvents = new List<IHistoryEvent>();

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateHistoryOrderCommand"/> class.
        /// </summary>
        /// <param name="dONumber">Define dONumber.</param>
        public CreateHistoryOrderCommand(string dONumber)
        {
            DOOrderNumber = dONumber;
        }

        /// <summary>
        /// Public parameter.
        /// </summary>
        public string DOOrderNumber { get; set; }

        /// <summary>
        /// Populate uncommiter event from IHistoryEvent member.
        /// </summary>
        /// <param name="evnt">Define IHistoryEvent member.</param>
        public void AddEvent(IHistoryEvent evnt)
        {
            ApplyEvent(evnt);
            _unCommitedEvents.Add(evnt);
        }

        /// <summary>
        /// Get All IHistoryEvent member.
        /// </summary>
        /// <returns>The <see cref="IHistoryEvent"/>.</returns>
        public IList<IHistoryEvent> GetEvent()
        {
            return _allEvents;
        }

        /// <summary>
        /// Apply Called Event.
        /// </summary>
        /// <param name="evnt">Define IHistoryEvent member.</param>
        public void ApplyEvent(IHistoryEvent evnt)
        {
            switch (evnt)
            {
                case HistoryCreatedEvent historyCreatedEvent:
                    Apply(historyCreatedEvent);
                    break;
                case HistoryConfirmedEvent historyConfirmedEvent:
                    Apply(historyConfirmedEvent);
                    break;
                case HistoryWaitingPaymentEvent historyWaitingPaymentEvent:
                    Apply(historyWaitingPaymentEvent);
                    break;
                case HistoryPaidEvent historyPaidEvent:
                    Apply(historyPaidEvent);
                    break;
            }

            _allEvents.Add(evnt);
        }

        /// <summary>
        /// Get all uncommited event from  IHistoryEvent.
        /// </summary>
        /// <returns>The <see cref="IHistoryEvent"/>.</returns>
        public IList<IHistoryEvent> GetUncommitedEvents()
        {
            return new List<IHistoryEvent>(_unCommitedEvents);
        }

        /// <summary>
        /// Get all event from  IHistoryEvent that exits on event store.
        /// </summary>
        /// <returns>The <see cref="IHistoryEvent"/>.</returns>
        public IList<IHistoryEvent> GetAllEvents()
        {
            return new List<IHistoryEvent>(_allEvents);
        }

        /// <summary>
        /// Clear list of uncomitted event.
        /// </summary>
        public void EventsCommitted()
        {
            _unCommitedEvents.Clear();
        }

        /// <summary>
        /// OnCreated Event.
        /// </summary>
        public void OnCreated()
        {
            var historyCreatedEvent = new HistoryCreatedEvent();
            AddEvent(historyCreatedEvent);
        }

        /// <summary>
        /// OnConfirmed Event.
        /// </summary>
        public void OnConfirmed()
        {
            var historyCreatedEvent = new HistoryCreatedEvent();
            AddEvent(historyCreatedEvent);
        }

        /// <summary>
        /// OnWaitingPayment Event.
        /// </summary>
        public void OnWaitingPayment()
        {
            var historyWaitingPaymentEvent = new HistoryWaitingPaymentEvent();
            AddEvent(historyWaitingPaymentEvent);
        }

        /// <summary>
        /// OnPaid Event.
        /// </summary>
        public void OnPaid()
        {
            var historyPaidEvent = new HistoryPaidEvent();
            AddEvent(historyPaidEvent);
        }

        private static void Apply(HistoryCreatedEvent historyCreatedEvent)
        {
            historyCreatedEvent.StateId = (int)EnumState.Created;
            historyCreatedEvent.StateName = Enum.GetName(typeof(EnumState), EnumState.Created);
            historyCreatedEvent.EventName = historyCreatedEvent.GetType().Name;
            historyCreatedEvent.Metadata = historyCreatedEvent.GetType().FullName;
            historyCreatedEvent.Created = DateTime.Now;
        }

        private static void Apply(HistoryConfirmedEvent historyConfirmedEvent)
        {
            historyConfirmedEvent.StateId = (int)EnumState.Created;
            historyConfirmedEvent.StateName = Enum.GetName(typeof(EnumState), EnumState.Created);
            historyConfirmedEvent.EventName = historyConfirmedEvent.GetType().Name;
            historyConfirmedEvent.Metadata = historyConfirmedEvent.GetType().FullName;
            historyConfirmedEvent.Created = DateTime.Now;
        }

        private static void Apply(HistoryWaitingPaymentEvent historyWaitingPaymentEvent)
        {
            historyWaitingPaymentEvent.StateId = (int)EnumState.Created;
            historyWaitingPaymentEvent.StateName = Enum.GetName(typeof(EnumState), EnumState.Created);
            historyWaitingPaymentEvent.EventName = historyWaitingPaymentEvent.GetType().Name;
            historyWaitingPaymentEvent.Metadata = historyWaitingPaymentEvent.GetType().FullName;
            historyWaitingPaymentEvent.Created = DateTime.Now;
        }

        private static void Apply(HistoryPaidEvent historyPaidEvent)
        {
            historyPaidEvent.StateId = (int)EnumState.Created;
            historyPaidEvent.StateName = Enum.GetName(typeof(EnumState), EnumState.Created);
            historyPaidEvent.EventName = historyPaidEvent.GetType().Name;
            historyPaidEvent.Metadata = historyPaidEvent.GetType().FullName;
            historyPaidEvent.Created = DateTime.Now;
        }
    }
}
