using AutoMapper;
using GoLogs.Framework.Mvc;
using GoLogs.Services.DeliveryOrder.Api.BusinessLogic;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GoLogs.Services.DeliveryOrder.Api.Controllers
{
    public class Controller : ControllerBase
    {
        private readonly IProblemCollector _problemCollector;

        protected readonly IDOOrderLogic DOOrderLogic;
        protected readonly IMapper Mapper;
        protected readonly IPublishEndpoint PublishEndpoint;

        public Controller(IDOOrderLogic doorderlogic, IProblemCollector problemCollector,
            IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _problemCollector = problemCollector;
            DOOrderLogic = doorderlogic;
            Mapper = mapper;
            PublishEndpoint = publishEndpoint;
        }

        protected ObjectResult CheckProblems()
        {
            if (!_problemCollector.HasProblems) return null;
            
            var problem = _problemCollector.GetProblems();
            return StatusCode(problem.Status.GetValueOrDefault(), problem);
        }
    }
}
