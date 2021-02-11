// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

using AutoMapper;
using GoLogs.Services.DeliveryOrder.Api.Models;

namespace GoLogs.Services.DeliveryOrder.Api
{
    /// <summary>
    /// Public class for mapper profile.
    /// </summary>
    public class DeliveryOrderMapperProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeliveryOrderMapperProfile"/> class.
        /// </summary>
        public DeliveryOrderMapperProfile()
        {
            CreateMap<DOOrderDto, DOOrder>();
        }
    }
}
