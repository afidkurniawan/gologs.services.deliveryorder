using GoLogs.Interfaces;
using MediatR;

// ReSharper disable InconsistentNaming
namespace GoLogs.Services.DeliveryOrder.Api.Commands
{
    public class CreateOrderCommand : IRequest<int>
    {
        public IDOOrder DOOrder { get; }

        public CreateOrderCommand() { }

        public CreateOrderCommand(IDOOrder doOrder)
        {
            DOOrder = doOrder;
        }
    }
}