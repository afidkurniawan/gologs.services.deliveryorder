// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------
using AutoMapper;
using GoLogs.Framework.Mvc;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GoLogs.Services.DeliveryOrder.Api.Controllers
{
    /// <summary>
    /// Base class controller.
    /// </summary>
    public class Controller : ControllerBase
    {
        private readonly IProblemCollector _problemCollector;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="Controller"/> class.
        /// </summary>
        /// <param name="problemCollector">Define IProblemCollector.</param>
        /// <param name="mapper">Define IMapper.</param>
        /// <param name="publishEndpoint">Define IPublishEndpoint.</param>
        /// <param name="mediator">Define IMediator.</param>
        public Controller(
            IProblemCollector problemCollector,
            IMapper mapper, IPublishEndpoint publishEndpoint, IMediator mediator)
        {
            _problemCollector = problemCollector;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
            _mediator = mediator;
        }

        /// <summary>
        /// Declare ObjectResult.
        /// </summary>
        /// <returns>ObjectResult.</returns>
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
