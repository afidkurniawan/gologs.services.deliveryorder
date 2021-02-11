// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

using AutoMapper;
using GoLogs.Services.DeliveryOrder.Api.Models;

namespace GoLogs.Services.DeliveryOrder.Api
{
    public class DeliveryOrderMapperProfile : Profile
    {
        public DeliveryOrderMapperProfile()
        {
            CreateMap<DOOrderDto, DOOrder>();
        }
    }
}
