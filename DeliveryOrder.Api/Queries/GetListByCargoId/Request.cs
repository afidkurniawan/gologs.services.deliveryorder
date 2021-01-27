﻿using GoLogs.Services.DeliveryOrder.Api.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoLogs.Services.DeliveryOrder.Api.Queries.GetListByCargoId
{
    public class Request : IRequest<IList<DOOrder>>
    {
        /// <summary>
        /// Get List of DOOrder associated with the specified Cargo Owner Id and paging
        /// Page : the page number of DOOrder data you want to display
        /// PageSize : the number of rows of DOOrder data on each page that you want to display
        /// </summary>
        public int CargoOwnerId { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }

    }
}
