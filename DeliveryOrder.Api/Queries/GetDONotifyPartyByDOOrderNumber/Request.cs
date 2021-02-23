// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

using GoLogs.Services.DeliveryOrder.Api.Models;
using MediatR;

namespace GoLogs.Services.DeliveryOrder.Api.Queries.GetDONotifyPartyByDOOrderNumber
{
    /// <summary>
    /// Declare public class request as parameter on controller to get DONotiifyOarty GetByDONumber.
    /// </summary>
    public class Request : IRequest<Response>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Request"/> class.
        /// </summary>
        /// <param name="dOOrderNumber">Define DOOrderNumber.</param>
        public Request(string dOOrderNumber)
        {
            DOOrderNumber = dOOrderNumber;
        }

        /// <summary>
        /// <c>GetAsync DONotifyParty </c>associated with the specified DOOrderNumber (string).
        /// </summary>
        public string DOOrderNumber { get; }
    }
}
