using GoLogs.Framework.Mvc;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GoLogs.Services.DeliveryOrder.Api.Controllers
{
    public class Controller : ControllerBase
    {
        private readonly IProblemCollector _problemCollector;

        protected readonly IMediator Mediator;
        
        public Controller(IProblemCollector problemCollector, IMediator mediator)
        {
            _problemCollector = problemCollector;
            Mediator = mediator;
        }

        protected ObjectResult CheckProblems()
        {
            if (!_problemCollector.HasProblems) return null;
            
            var problem = _problemCollector.GetProblems();
            return StatusCode(problem.Status.GetValueOrDefault(), problem);
        }
    }
}
