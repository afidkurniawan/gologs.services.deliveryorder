// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using GoLogs.Framework.Mvc;
using GoLogs.Services.DeliveryOrder.Api.Application.Internals;
using GoLogs.Services.DeliveryOrder.Api.Models;
using MediatR;
using Nirbito.Framework.Core;
using SqlKata;

namespace GoLogs.Services.DeliveryOrder.Api.Commands
{
    /// <summary>
    /// Class to create DOOrderNumber.
    /// </summary>
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, DOOrder>
    {
        private readonly IDOOrderContext _context;
        private readonly IProblemCollector _problemCollector;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateOrderCommandHandler"/> class.
        /// </summary>
        /// <param name="context">Define DOOrderContext.</param>
        /// <param name="problemCollector">Define IProblemCollector.</param>
        public CreateOrderCommandHandler(IDOOrderContext context, IProblemCollector problemCollector)
        {
            _context = context;
            _problemCollector = problemCollector;
        }

        /// <summary>
        /// Handle to get Create DOOrderNumber.
        /// </summary>
        /// <param name="request">Define request.</param>
        /// <param name="cancellationToken">Specified CancellationToken.</param>
        /// <returns>The <see cref="DOOrder"/>.</returns>
        public async Task<DOOrder> Handle(CreateOrderCommand request, CancellationToken cancellationToken = default)
        {
            var dOOrder = new DOOrder();
            var transactionOptions = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted,
                Timeout = TransactionManager.MaximumTimeout
            };

            using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew, transactionOptions, TransactionScopeAsyncFlowOption.Enabled))
            {
                Check.NotNull(request, nameof(request));
                var lastData = await _context.DOOrders.FirstOrDefaultAsync(
                    new Query().Select(nameof(DOOrder.Id)).OrderByDesc(nameof(DOOrder.Id)),
                    cancellationToken);

                var lastId = 0;
                if (lastData != null)
                {
                    lastId = lastData.Id;
                }

                lastId += 1;
                var dOOrderNumber = "DO" + lastId;
                dOOrder.DOOrderNumber = dOOrderNumber;
                dOOrder.CargoOwnerId = request.CargoOwnerId;
                await _context.DOOrders.InsertAsync(dOOrder, cancellationToken);
                scope.Complete();
                scope.Dispose();
            }

            return dOOrder;
        }
    }
}
