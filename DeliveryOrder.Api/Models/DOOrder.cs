using System.ComponentModel.DataAnnotations;
namespace GoLogs.Services.DeliveryOrder.Api.Models
{

    public class DOOrder : DOOrderDto, IEntity
    {
        /// <summary>
        /// DOOrder Entity model
        /// </summary>
        [Key]
        public int Id { get; set; }
        public string DoOrderNumber { get; set; }
        
    }
}
