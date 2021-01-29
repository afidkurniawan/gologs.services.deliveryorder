using GoLogs.Services.DeliveryOrder.Api.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoLogs.Services.DeliveryOrder.Api.Queries.GetByCargoOwnerId
{
    public class Request : IRequest<IList<DOOrder>>
    {
        public Request(int cargoOwnerId)
        {
            CargoOwnerId = cargoOwnerId;
        }

        /// <summary>
        /// Get List of DOOrder associated with the specified Cargo Owner Id and paging
        /// Page : the page number of DOOrder data you want to display
        /// PageSize : the number of rows of DOOrder data on each page that you want to display
        /// </summary>
        public int CargoOwnerId { get; }        

    }
}
