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
using System;

namespace GoLogs.Services.DeliveryOrder.Api.Commands
{
    public class CreateOrderCommandHandler : IRequestHandler<DOOrder, int>
    {
        private readonly DOOrderContext _context;
        private readonly IProblemCollector _problemCollector;

        public CreateOrderCommandHandler(DOOrderContext context, IProblemCollector problemCollector)
        {
            _context = context;
            _problemCollector = problemCollector;
        }
        /// <summary>
        /// Handle for Create DOOrder Number
        /// </summary>
        /// <param name="createOrderCommand"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
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
                var lastdata = await _context.DOOrders.FirstOrDefaultAsync(new Query().Select("id").OrderByDesc("id"), cancellationToken);
                var lastid = lastdata.Id;// 0;// lastdata[0].Id;
                lastid += 1;
                var dOOrderNumber = "DO" + lastid;
                createOrderCommand.DOOrderNumber = dOOrderNumber;
                await _context.DOOrders.InsertAsync(createOrderCommand, cancellationToken);
                scope.Complete();
                scope.Dispose();
            }
            return await Task.FromResult(1);
        }
    }
}
