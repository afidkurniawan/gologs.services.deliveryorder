using System.ComponentModel.DataAnnotations;
namespace GoLogs.Services.DeliveryOrder.Api.Models
{
    public class History : HistoryDto, IEntity
    {
        [Key]
        public int Id { get; set ; }
     
    }
}
