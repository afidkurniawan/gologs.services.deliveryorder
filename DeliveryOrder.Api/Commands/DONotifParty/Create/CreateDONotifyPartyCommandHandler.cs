// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using GoLogs.Framework.Mvc;
using GoLogs.Services.DeliveryOrder.Api.Application.Internals;
using GoLogs.Services.DeliveryOrder.Api.Models;
using MediatR;
using Nirbito.Framework.Core;
using SqlKata;

namespace GoLogs.Services.DeliveryOrder.Api.Commands.DONotifParty.Create
{
    /// <summary>
    /// Class to create NotifyParty.
    /// </summary>
    public class CreateDONotifyPartyCommandHandler : IRequestHandler<CreateDONotifyPartyCommand, CreateDONotifyPartyResponse>
    {
        private readonly DOOrderContext _context;
        private readonly IProblemCollector _problemCollector;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateDONotifyPartyCommandHandler"/> class.
        /// </summary>
        /// <param name="context">Define DOOrderContext.</param>
        /// <param name="problemCollector">Define IProblemCollector.</param>
        public CreateDONotifyPartyCommandHandler(DOOrderContext context, IProblemCollector problemCollector)
        {
            _context = context;
            _problemCollector = problemCollector;
        }

        /// <summary>
        /// Handle to Create DO Notify Party.
        /// </summary>
        /// <param name="request">Define request.</param>
        /// <param name="cancellationToken">Specified CancellationToken.</param>
        /// <returns>The List <see cref="DONotifyParty"/>.</returns>
        public async Task<CreateDONotifyPartyResponse> Handle(CreateDONotifyPartyCommand request, CancellationToken cancellationToken)
        {
            var party = new CreateDONotifyPartyResponse { DOOrderNumber = request.DOOrderNumber, NotifyAddress = new List<string>() };
            var notifyParty = new DONotifyParty();

            var transactionOptions = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted,
                Timeout = TransactionManager.MaximumTimeout
            };

            using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew, transactionOptions, TransactionScopeAsyncFlowOption.Enabled))
            {
                Check.NotNull(request, nameof(request));

                var dONumber = await _context.DOOrders.FirstOrDefaultAsync(
                    new Query().Where(nameof(DOOrder.DOOrderNumber), request.DOOrderNumber),
                    cancellationToken);

                if (dONumber == null)
                {
                    return null; // not found
                }

                var notifyPrty = await _context.DONotifyParties.FirstOrDefaultAsync(
                    new Query().Where(nameof(DONotifyParty.DOOrderNumber), request.DOOrderNumber),
                    cancellationToken);

                if (notifyPrty == null)
                {
                    var emails = String.Empty;
                    foreach (var email in request.NotifyAddress)
                    {
                        if (emails == String.Empty)
                        {
                            emails = email;
                            party.NotifyAddress.Add(new string(email));
                        }
                        else
                        {
                            if (!emails.Contains(email, StringComparison.OrdinalIgnoreCase))
                            {
                                emails += ";" + email;
                                party.NotifyAddress.Add(new string(email));
                            }
                        }
                    }

                    notifyParty.DOOrderNumber = request.DOOrderNumber;
                    notifyParty.NotifyAddress = emails;
                    await _context.DONotifyParties.InsertAsync(notifyParty, cancellationToken);
                    party.Id = notifyParty.Id;
                }
                else
                {
                    return new CreateDONotifyPartyResponse(); // conflic
                }

                scope.Complete();
                scope.Dispose();
            }

            return party;
        }
    }
}
