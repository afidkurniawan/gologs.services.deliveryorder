using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoLogs.Services.DeliveryOrder.Api.Models
{
    public interface IEntity
    {
        /// <summary>
        /// Mandatory Field for each entity
        /// </summary>
        int Id { get; set; }
    }
}
