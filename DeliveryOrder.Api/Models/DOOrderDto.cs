using MediatR;
namespace GoLogs.Services.DeliveryOrder.Api.Models
{
    public class DOOrderDto : IRequest<int>
    {
        /// <summary>
        /// CargoOwnerId : the Id of DOOrder request
        /// </summary>
        public int CargoOwnerId { get; set; }
    }
}
