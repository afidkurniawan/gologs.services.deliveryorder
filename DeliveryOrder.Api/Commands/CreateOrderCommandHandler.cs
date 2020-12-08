using System.Threading;
using System.Threading.Tasks;
using GoLogs.Framework.Mvc;
using MediatR;
using Nirbito.Framework.Core;

namespace GoLogs.Services.DeliveryOrder.Api.Commands
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, int>
    {
        private IProblemCollector _problemCollector;

        public CreateOrderCommandHandler(IProblemCollector problemCollector)
        {
            _problemCollector = problemCollector;
        }
        
        public async Task<int> Handle(CreateOrderCommand createOrderCommand, CancellationToken cancellationToken = default)
        {
            Check.NotNull(createOrderCommand, nameof(createOrderCommand));
            
            // return await _context.DOOrders.Insert(createOrderCommand.DOOrder);
            
            return await Task.FromResult(1);
        }
    }
}