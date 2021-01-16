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
        private DOOrderContext _context;
        private IProblemCollector _problemCollector;

        public CreateOrderCommandHandler(DOOrderContext context, IProblemCollector problemCollector)
        {
            _context = context;
            _problemCollector = problemCollector;
        }
        
        public async Task<int> Handle(DOOrder createOrderCommand, CancellationToken cancellationToken = default)
        {
            Check.NotNull(createOrderCommand, nameof(createOrderCommand));
            var cid = createOrderCommand.CargoOwnerId;
            var a = await _context.Doorders.AllAsync(new Query().Where(nameof(createOrderCommand.CargoOwnerId), cid));
            int lastId = a.Count + 1;
            var doNumber = "DO" + lastId;
            createOrderCommand.DoOrderNumber = doNumber;            
            await _context.Doorders.InsertAsync(createOrderCommand);            
            return await Task.FromResult(1);
        }
    }
}