// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

using GoLogs.Services.DeliveryOrder.Api.Models;
using MediatR;

namespace GoLogs.Services.DeliveryOrder.Api.Queries.GetByNumber
{
    public class Request : IRequest<DOOrder>
    {
        public Request(string doNumber)
        {
            DoNumber = doNumber;
        }

        /// <summary>
        /// <c>GetAsync DOOrder </c>associated with the specified DOOrderNumber (string).
        /// </summary>
        public string DoNumber { get; }
    }
}
