using MediatR;
namespace GoLogs.Services.DeliveryOrder.Api.Models
{
    public class DOOrderDto : IRequest<int>
    {
        /// <summary>
        /// CargoOwnerId is a mandatory Parameter to create DOOrder Number
        /// </summary>
        public int CargoOwnerId { get; set; }
    }
}
