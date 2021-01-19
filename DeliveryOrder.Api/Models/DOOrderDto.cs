using MediatR;
namespace GoLogs.Services.DeliveryOrder.Api.Models
{
    public class DOOrderDto : IRequest<int>
    {
        /// <summary>
        /// ID customer pengaju  
        /// </summary>
        public int CargoOwnerId { get; set; }
    }
}
