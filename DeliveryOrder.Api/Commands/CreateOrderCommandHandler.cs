using System.Threading;
using System.Threading.Tasks;
using GoLogs.Framework.Mvc;
using GoLogs.Services.DeliveryOrder.Api.Application.Internals;
using GoLogs.Services.DeliveryOrder.Api.Models;
using MediatR;
using Nirbito.Framework.Core;
using SqlKata;

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
            Check.NotNull(createOrderCommand, nameof(createOrderCommand));            
            var lastdata = await _context.DOOrders.AllAsync(new Query().SelectRaw("max(id) "));
            int lastid = lastdata[0].Id;
            lastid += 1;
            var DOOrderNumber = "DO" + lastid;
            createOrderCommand.DOOrderNumber = DOOrderNumber;            
            await _context.DOOrders.InsertAsync(createOrderCommand);            
            return await Task.FromResult(1);
        }

    }
}