// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

using System;
using FluentValidation;
using GoLogs.Services.DeliveryOrder.Api.Models;

namespace GoLogs.Services.DeliveryOrder.Api.Application.Validators
{
    public class DOOrderValidator : AbstractValidator<DOOrderDto>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DOOrderValidator"/> class.
        /// To Validate parameter when create DOOrderNumber.
        /// </summary>
        public DOOrderValidator()
        {
            RuleFor(c => c.CargoOwnerId)
                .NotNull()
                .InclusiveBetween(1, Int32.MaxValue);
        }
    }
}
