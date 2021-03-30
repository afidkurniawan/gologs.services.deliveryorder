// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

using FluentValidation.TestHelper;
using GoLogs.Services.DeliveryOrder.Api.Application.Validators;
using GoLogs.Services.DeliveryOrder.Api.Models;
using Xunit;

namespace GoLogs.Services.DeliveryOrder.Api.UnitTests.Validators
{
    // ReSharper disable once InconsistentNaming
    public class DOOrderValidatorTests
    {
        private readonly DOOrderValidator _validator;

        public DOOrderValidatorTests()
        {
            _validator = new DOOrderValidator();
        }

        [Fact]
        public void CargoOwnerId_must_be_positive_integer()
        {
            var order = new DOOrder();
            _validator.TestValidate(order).ShouldHaveValidationErrorFor(o => o.CargoOwnerId);

            order.CargoOwnerId = -1;
            _validator.TestValidate(order).ShouldHaveValidationErrorFor(o => o.CargoOwnerId);

            order.CargoOwnerId = 0;
            _validator.TestValidate(order).ShouldHaveValidationErrorFor(o => o.CargoOwnerId);

            order.CargoOwnerId = 1;
            _validator.TestValidate(order).ShouldNotHaveValidationErrorFor(o => o.CargoOwnerId);
        }
    }
}
