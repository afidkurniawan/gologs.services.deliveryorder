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
    /// <summary>
    /// Public class for Create Order.
    /// </summary>
    public class CreateOrderCommand : IRequest<int>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateOrderCommand"/> class.
        /// </summary>
        /// <param name="doOrder">Define ICreateDOOrder.</param>
        public CreateOrderCommand(ICreateDOOrder doOrder)
        {
            DoOrder = doOrder;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateOrderCommand"/> class.
        /// </summary>
        public CreateOrderCommand() { }

        private ICreateDOOrder DoOrder { get; }
    }
}
