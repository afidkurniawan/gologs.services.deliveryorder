using GoLogs.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
namespace GoLogs.Services.DeliveryOrder.Api.Models
{

    public class DOOrder : DOOrderDto, IDOOrder
    {
        [Key]
        public int Id { get; set; }
        public string DOOrderNumber { get; set; }
        
    }
}
