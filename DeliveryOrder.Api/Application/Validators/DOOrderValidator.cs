﻿using FluentValidation;
using GoLogs.Services.DeliveryOrder.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoLogs.Services.DeliveryOrder.Api.Application.Validators
{
    public class DOOrderValidator : AbstractValidator<DOOrderDto>
    {
        /// <summary>
        /// To Validate parameter when create DOOrderNumber
        /// </summary>
        public DOOrderValidator() {
            RuleFor(c => c.CargoOwnerId)
                .NotNull()
                .InclusiveBetween(1, Int32.MaxValue);
        }
    }
}
