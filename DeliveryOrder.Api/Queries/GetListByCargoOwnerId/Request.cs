using GoLogs.Services.DeliveryOrder.Api.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoLogs.Services.DeliveryOrder.Api.Queries.GetListByCargoOwnerId
{
    public class Request : IRequest<IList<DOOrder>>
    {
        public Request() { 
        }
        public Request(int cargoOwnerId, int page, int pageSize)
        {
            CargoOwnerId = cargoOwnerId;
            Page = page;
            PageSize = pageSize;
        }

        /// <summary>
        /// Get List of DOOrder associated with the specified Cargo Owner Id and paging
        /// Page : the page number of DOOrder data you want to display
        /// PageSize : the number of rows of DOOrder data on each page that you want to display
        /// </summary>
        [BindProperty(Name = "cargoOwnerId")]
        public int CargoOwnerId { get; }
        [BindProperty(Name = "page")]
        public int Page { get; }
        [BindProperty(Name = "pageSize")]
        public int PageSize { get; }

    }
}
