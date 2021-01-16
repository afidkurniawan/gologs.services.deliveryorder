﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoLogs.Services.DeliveryOrder.Api.Models
{
    public class DOOrderDto : IRequest<int>
    {
        /// <summary>
        /// ID customer pengaju  
        /// </summary>
        public int CargoOwnerId { get; set; }
    }
}
