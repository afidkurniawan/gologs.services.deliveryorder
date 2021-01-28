using GoLogs.Services.DeliveryOrder.Api.Models;
using MediatR;

// ReSharper disable InconsistentNaming
namespace GoLogs.Services.DeliveryOrder.Api.Commands
{
    public class CreateOrderCommand : IRequest<int>
    {
        //public DOOrder dorequest { get; }
        ICreateDOOrder DoOrder { get; }

        public CreateOrderCommand() { }

        public CreateOrderCommand(ICreateDOOrder doOrder)
        {
            DoOrder = doOrder;
        }
    }
}