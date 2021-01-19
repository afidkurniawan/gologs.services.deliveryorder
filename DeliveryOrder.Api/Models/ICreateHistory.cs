using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoLogs.Services.DeliveryOrder.Api.Models
{
    public interface ICreateHistory :IEntity
    {
        public string DoNumber { get; }
        public int StateId { get; }
    }
}
