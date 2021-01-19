using System.ComponentModel.DataAnnotations;
namespace GoLogs.Services.DeliveryOrder.Api.Models
{

    public class DOOrder : DOOrderDto, IEntity
    {
        [Key]
        public int Id { get; set; }
        public string DoOrderNumber { get; set; }
        
    }
}
