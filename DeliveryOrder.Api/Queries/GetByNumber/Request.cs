// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

using GoLogs.Services.DeliveryOrder.Api.Models;
using MediatR;

namespace GoLogs.Services.DeliveryOrder.Api.Queries.GetByNumber
{
    /// <summary>
    /// Declare public class request as parameter on controller to get DOOrder GetByNumber.
    /// </summary>
    public class Request : IRequest<DOOrder>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Request"/> class.
        /// </summary>
        /// <param name="doNumber">Define DoNumber.</param>
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
