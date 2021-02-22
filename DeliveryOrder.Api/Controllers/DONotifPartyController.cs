// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

using System.Threading.Tasks;
using AutoMapper;
using GoLogs.Framework.Mvc;
using GoLogs.Interfaces;
using GoLogs.Services.DeliveryOrder.Api.Commands.DONotifParty.Create;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoLogs.Services.DeliveryOrder.Api.Controllers
{
    /// <summary>
    /// Public controller DOOrdersController.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class DONotifPartyController : Controller
    {
        private readonly IProblemCollector _problemCollector;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="DONotifPartyController"/> class.
        /// </summary>
        /// <param name="problemCollector">Define IProblemCollector.</param>
        /// <param name="mapper">Define IMapper.</param>
        /// <param name="publishEndpoint">Define IPublishEndpoint.</param>
        /// <param name="mediator">Define IMediator.</param>
        public DONotifPartyController(IProblemCollector problemCollector, IMapper mapper,
            IPublishEndpoint publishEndpoint, IMediator mediator)
            : base(problemCollector, mapper, publishEndpoint, mediator)
        {
            _problemCollector = problemCollector;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
            _mediator = mediator;
        }

        /// <summary>
        /// Create DO Notify Party And Publish IDONotifyParty to Rabbit MQ.
        /// </summary>
        /// <param name="dONotifyParty">Specified DOOrderNumber (string).</param>
        /// <returns>The List<see cref="IDONotifyParty"/>.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> CreateAsync([FromBody] CreateDONotifyPartyCommand dONotifyParty)
        {
            var errorResult = CheckProblems();
            if (dONotifyParty.NotifyAddress == null)
            {
                return BadRequest();
            }

            var result = await _mediator.Send(dONotifyParty);
            if (result == null)
            {
                return NotFound();
            }

            if (result.Id == 0)
            {
                return Conflict();
            }

            await _publishEndpoint.Publish<IDONotifyParty>(new { result.DOOrderNumber, result.NotifyAddress });
            return errorResult ?? Ok(result);
        }
    }
}
