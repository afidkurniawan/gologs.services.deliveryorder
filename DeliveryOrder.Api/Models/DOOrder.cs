using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoLogs.Services.DeliveryOrder.Api.Models
{

    public class DOOrder : DOOrderDto, IEntity
    {
        [Key]
        public int Id { get; set; }
        public string DoOrderNumber { get; set; }
        
    }
}
