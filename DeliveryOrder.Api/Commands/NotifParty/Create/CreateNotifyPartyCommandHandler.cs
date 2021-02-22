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

namespace GoLogs.Services.DeliveryOrder.Api.Commands.NotifParty.Create
{
    /// <summary>
    /// Class to create NotifyParty.
    /// </summary>
    public class CreateNotifyPartyCommandHandler : IRequestHandler<CreateNotifyPartyCommand, CreateNotifyPartyResponse>
    {
        private readonly DOOrderContext _context;
        private readonly IProblemCollector _problemCollector;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateNotifyPartyCommandHandler"/> class.
        /// </summary>
        /// <param name="context">Define DOOrderContext.</param>
        /// <param name="problemCollector">Define IProblemCollector.</param>
        public CreateNotifyPartyCommandHandler(DOOrderContext context, IProblemCollector problemCollector)
        {
            _context = context;
            _problemCollector = problemCollector;
        }

        /// <summary>
        /// Handle to Create Notify Party.
        /// </summary>
        /// <param name="request">Define request.</param>
        /// <param name="cancellationToken">Specified CancellationToken.</param>
        /// <returns>The List <see cref="NotifyParty"/>.</returns>
        public async Task<CreateNotifyPartyResponse> Handle(CreateNotifyPartyCommand request, CancellationToken cancellationToken)
        {
            var party = new CreateNotifyPartyResponse { CargoOwnerId = request.CargoOwnerId, NotifyAddress = new List<string>() };
            var notifyParty = new NotifyParty();
            var transactionOptions = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted,
                Timeout = TransactionManager.MaximumTimeout
            };

            using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew, transactionOptions, TransactionScopeAsyncFlowOption.Enabled))
            {
                Check.NotNull(request, nameof(request));
                var notifyPrty = await _context.NotifyParties.FirstOrDefaultAsync(
                    new Query().Where(nameof(NotifyParty.CargoOwnerId), request.CargoOwnerId),
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

                    notifyParty.CargoOwnerId = request.CargoOwnerId;
                    notifyParty.NotifyAddress = emails;
                    await _context.NotifyParties.InsertAsync(notifyParty, cancellationToken);
                    party.Id = notifyParty.Id;
                }
                else
                {
                    return null;
                }

                scope.Complete();
                scope.Dispose();
            }

            return party;
        }
    }
}
