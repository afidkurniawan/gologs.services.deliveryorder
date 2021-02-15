// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

using Microsoft.AspNetCore.Http;

namespace GoLogs.Services.DeliveryOrder.Api.Application.Internals
{
    /// <summary>
    /// Public clas ScopedHttpContext.
    /// </summary>
    public class ScopedHttpContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScopedHttpContext"/> class.
        /// </summary>
        /// <param name="httpContextAccessor">Define IHttpContextAccessor.</param>
        public ScopedHttpContext(IHttpContextAccessor httpContextAccessor)
        {
            Accessor = httpContextAccessor;
        }

        /// <summary>
        /// Public Properties for IHttpContextAccessor.
        /// </summary>
        public IHttpContextAccessor Accessor { get; }
    }
}
