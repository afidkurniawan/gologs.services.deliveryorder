// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

using GoLogs.Framework.Mvc;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GoLogs.Services.DeliveryOrder.Api.Controllers
{
    public class Controller : ControllerBase
    {
        private readonly IProblemCollector _problemCollector;

        public Controller(IProblemCollector problemCollector, IMediator mediator)
        {
            _problemCollector = problemCollector;
            Mediator = mediator;
        }

        protected IMediator Mediator { get; }

        protected ObjectResult CheckProblems()
        {
            if (!_problemCollector.HasProblems)
            {
                return null;
            }

            var problem = _problemCollector.GetProblems();
            return StatusCode(problem.Status.GetValueOrDefault(), problem);
        }
    }
}
