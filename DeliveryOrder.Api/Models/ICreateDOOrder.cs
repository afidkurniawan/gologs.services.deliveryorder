using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoLogs.Services.DeliveryOrder.Api.Models
{
   public interface ICreateDOOrder : IEntity
    {      
        public int CargoOwnerId { get; }
    }
}
