// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

using System;
using System.Collections.Generic;
using GoLogs.Contracts.Events;
using GoLogs.Services.DeliveryOrder.Api.Enums;
using GoLogs.Services.DeliveryOrder.Api.Events;
using GoLogs.Services.DeliveryOrder.Api.Models;
using MediatR;

namespace GoLogs.Services.DeliveryOrder.Api.Commands
{
    public class CurrentState
    {
        public EnumState State { get; set; }
    }
}
