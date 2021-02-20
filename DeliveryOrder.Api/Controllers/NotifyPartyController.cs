// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------

using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GoLogs.Events;
using GoLogs.Framework.Mvc;
using GoLogs.Interfaces;
using GoLogs.Services.DeliveryOrder.Api.Commands.NofityParty.Create;
using GoLogs.Services.DeliveryOrder.Api.Models;
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
    public class NotifyPartyController : Controller
    {
        private readonly IProblemCollector _problemCollector;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotifyPartyController"/> class.
        /// </summary>
        /// <param name="problemCollector">Define IProblemCollector.</param>
        /// <param name="mapper">Define IMapper.</param>
        /// <param name="publishEndpoint">Define IPublishEndpoint.</param>
        /// <param name="mediator">Define IMediator.</param>
        public NotifyPartyController(IProblemCollector problemCollector, IMapper mapper,
            IPublishEndpoint publishEndpoint, IMediator mediator)
            : base(problemCollector, mapper, publishEndpoint, mediator)
        {
            _problemCollector = problemCollector;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
            _mediator = mediator;
        }

        /// <summary>
        /// Create Notify Party And Publish NotifyPartyCreatedEvent to Rabbit MQ.
        /// </summary>
        /// <param name="notifyParty">Specified CargoOwnerId (int).</param>
        /// <returns>The List<see cref="INotifyParty"/>.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult> CreateAsync([FromBody] CreateNotifyPartyCommand notifyParty)
        {
            var errorResult = CheckProblems();
            if (notifyParty.CargoOwnerId < 1 || notifyParty.NotifyAddress == null)
            {
                return BadRequest();
            }

            var result = await _mediator.Send(notifyParty);
            if (result == null)
            {
                return Conflict();
            }

            await _publishEndpoint.Publish<INotifyParty>(new { result.CargoOwnerId, result.NotifyAddress });
            return errorResult ?? Ok(result);
        }
    }
}
