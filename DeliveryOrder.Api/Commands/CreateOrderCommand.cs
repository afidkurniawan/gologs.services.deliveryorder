// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

using GoLogs.Interfaces;
using MediatR;

// ReSharper disable InconsistentNaming
namespace GoLogs.Services.DeliveryOrder.Api.Commands
{
    public class CreateOrderCommand : IRequest<int>
    {
        public CreateOrderCommand() { }

        public CreateOrderCommand(IDOOrder doOrder)
        {
            DOOrder = doOrder;
        }

        public IDOOrder DOOrder { get; }
    }
}
