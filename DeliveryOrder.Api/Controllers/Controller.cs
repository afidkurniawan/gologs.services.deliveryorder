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
    public class Controller : ControllerBase
    {

        private readonly IProblemCollector ProblemCollector;        
        protected readonly IMapper Mapper;
        protected readonly IPublishEndpoint PublishEndpoint;
        protected readonly IMediator Mediator;
        public Controller(IProblemCollector problemCollector,
            IMapper mapper, IPublishEndpoint publishEndpoint, IMediator mediator)
        {
            ProblemCollector = problemCollector;
            Mapper = mapper;
            PublishEndpoint = publishEndpoint;
            Mediator = mediator;
        }
        protected ObjectResult CheckProblems()
        {
            if (!ProblemCollector.HasProblems)
                return null;

            var problem = ProblemCollector.GetProblems();
            return StatusCode(problem.Status.GetValueOrDefault(), problem);
        }
    }
}
