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
