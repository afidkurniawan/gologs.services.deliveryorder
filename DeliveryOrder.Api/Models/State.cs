using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoLogs.Services.DeliveryOrder.Api.Models
{
    public class State : IEntity
    {
        /// <summary>
        /// State Entity model
        /// </summary>
        [Key]
        public int Id { get ; set ; }
        public string StateName { get; set; }
    }
}
