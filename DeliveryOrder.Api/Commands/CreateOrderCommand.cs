// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------
using GoLogs.Services.DeliveryOrder.Api.Models;
using MediatR;

// ReSharper disable InconsistentNaming
namespace GoLogs.Services.DeliveryOrder.Api.Commands
{
    public class CreateOrderCommand : IRequest<int>
    {
        //public DOOrder dorequest { get; }
        private ICreateDOOrder DoOrder { get; }

        public CreateOrderCommand() { }

        public CreateOrderCommand(ICreateDOOrder doOrder)
        {
            DoOrder = doOrder;
        }
    }
}
