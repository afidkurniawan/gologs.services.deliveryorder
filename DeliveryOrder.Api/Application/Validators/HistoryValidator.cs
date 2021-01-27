using FluentValidation;
using GoLogs.Services.DeliveryOrder.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoLogs.Services.DeliveryOrder.Api.Application.Validators
{
    public class HistoryValidator : AbstractValidator<HistoryDto>
    {
        /// <summary>
        /// To Validate parameter when create history
        /// </summary>
        public HistoryValidator() {
            RuleFor(c => c.DOOrderNumber).NotEmpty().WithMessage("Please specify a DO Order Number");               
        }
    }
}
