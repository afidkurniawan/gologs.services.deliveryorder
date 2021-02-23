// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using GoLogs.Framework.Mvc;
using GoLogs.Services.DeliveryOrder.Api.Application.Internals;
using GoLogs.Services.DeliveryOrder.Api.Models;
using MediatR;
using Nirbito.Framework.Core;
using SqlKata;

namespace GoLogs.Services.DeliveryOrder.Api.Commands.DONotifParty.Update
{
    /// <summary>
    /// Class to create NotifyParty.
    /// </summary>
    public class UpdateDONotifyPartyCommandHandler : IRequestHandler<UpdateDONotifyPartyCommand, UpdateDONotifyPartyResponse>
    {
        private readonly DOOrderContext _context;
        private readonly IProblemCollector _problemCollector;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateDONotifyPartyCommandHandler"/> class.
        /// </summary>
        /// <param name="context">Define DOOrderContext.</param>
        /// <param name="problemCollector">Define IProblemCollector.</param>
        public UpdateDONotifyPartyCommandHandler(DOOrderContext context, IProblemCollector problemCollector)
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
        public async Task<UpdateDONotifyPartyResponse> Handle(UpdateDONotifyPartyCommand request, CancellationToken cancellationToken)
        {
            var notifyPrty = new DONotifyParty();
            var transactionOptions = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted,
                Timeout = TransactionManager.MaximumTimeout
            };

            using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew, transactionOptions, TransactionScopeAsyncFlowOption.Enabled))
            {
                Check.NotNull(request, nameof(request));
                notifyPrty = await _context.DONotifyParties.FirstOrDefaultAsync(
                    new Query().Where(nameof(DONotifyParty.DOOrderNumber), request.DOOrderNumber),
                    cancellationToken);

                if (notifyPrty != null)
                {
                    var emails = notifyPrty.NotifyAddress;
                    foreach (var email in request.NotifyAddress)
                    {
                        if (emails == String.Empty)
                        {
                            emails += email;
                        }
                        else
                        {
                            if (!emails.Contains(email, StringComparison.OrdinalIgnoreCase))
                            {
                                emails += ";" + email;
                            }
                        }
                    }

                    notifyPrty.NotifyAddress = emails;
                    await _context.DONotifyParties.UpdateAsync(notifyPrty, cancellationToken);
                }
                else
                {
                    return null;
                }

                scope.Complete();
                scope.Dispose();
            }

            return new UpdateDONotifyPartyResponse { Id = notifyPrty.Id, DOOrderNumber = notifyPrty.DOOrderNumber, NotifyAddress = notifyPrty.NotifyAddress.Split(';').ToList() };
        }
    }
}
