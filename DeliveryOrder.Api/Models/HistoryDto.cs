﻿using GoLogs.Services.DeliveryOrder.Api.Enum;
using MediatR;
namespace GoLogs.Services.DeliveryOrder.Api.Models
{
    public class HistoryDto : IRequest<int>
    {
        public string DoNumber { get; set; }
        public int StateId { get; set; }
    }
}
