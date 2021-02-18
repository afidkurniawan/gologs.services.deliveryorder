// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

using MediatR;

namespace GoLogs.Services.DeliveryOrder.Api.Models
{
    /// <summary>
    /// Initialize DOOrderDto class.
    /// </summary>
    public class HistoryDto : IRequest<string>
    {
        /// <summary>
        /// DOOrderNumber is a mandatory Parameter to CREATE History.
        /// </summary>
        public string DOOrderNumber { get; set; }
    }
}
