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
    public class CreateOrderCommandHandler : IRequestHandler<DOOrder, int>
    {
        private readonly DOOrderContext _context;
        private readonly IProblemCollector _problemCollector;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateOrderCommandHandler"/> class.
        /// </summary>
        /// <param name="context">Define DOOrderContext.</param>
        /// <param name="problemCollector">Define IProblemCollector.</param>
        public CreateOrderCommandHandler(DOOrderContext context, IProblemCollector problemCollector)
        {
            _context = context;
            _problemCollector = problemCollector;
        }

        /// <summary>
        /// Handle to get Create DOOrderNumber.
        /// </summary>
        /// <param name="createOrderCommand">Specified DOOrder.</param>
        /// <param name="cancellationToken">Specified CancellationToken.</param>
        /// <returns>integer.</returns>
        public async Task<int> Handle(DOOrder createOrderCommand, CancellationToken cancellationToken = default)
        {
            var transactionOptions = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted,
                Timeout = TransactionManager.MaximumTimeout
            };

            using (var scope = new TransactionScope(TransactionScopeOption.RequiresNew, transactionOptions, TransactionScopeAsyncFlowOption.Enabled))
            {
                Check.NotNull(createOrderCommand, nameof(createOrderCommand));
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
                createOrderCommand.DOOrderNumber = dOOrderNumber;
                await _context.DOOrders.InsertAsync(createOrderCommand, cancellationToken);
                scope.Complete();
                scope.Dispose();
            }

            return await Task.FromResult(1);
        }
    }
}
