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
using GoLogs.Services.DeliveryOrder.Api.Commands.NofityParty.Update;
using GoLogs.Services.DeliveryOrder.Api.Models;
using GoLogs.Services.DeliveryOrder.Api.Queries;
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
        /// Get Notify Party associated with the specified Cargo Owner Id (optional).
        /// </summary>
        /// <param name="cargoOwnerId">Specified CargoOwnerId (int).</param>
        /// <returns>The <see cref="INotifyParty"/>.</returns>
        [HttpGet]
        [Route("{cargoOwnerId:int}")]
        [ProducesResponseType(typeof(INotifyParty), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<INotifyParty>> GetAsync(int cargoOwnerId)
        {
            var errorResult = CheckProblems();
            if (cargoOwnerId <= 0)
            {
                return BadRequest();
            }

            var response = await _mediator.Send(new Queries.GetNotifyPartyByCargoOwnerId.Request(cargoOwnerId));
            if (response == null)
            {
                return NotFound();
            }

            return errorResult ?? Ok(response);
        }

        /// <summary>
        /// Get List of Notify Party associated with the specified Cargo Owner Id (optional).
        /// </summary>
        /// <param name="page">Specified Page (int).</param>
        /// <param name="pageSize">Specified PageSize (int).</param>
        /// <returns>The List of <see cref="INotifyParty"/>.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<INotifyParty>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<INotifyParty>>> GetAsync([FromQuery] int page, [FromQuery] int pageSize)
        {
            var errorResult = CheckProblems();
            if (page <= 0 || pageSize <= 0)
            {
                return BadRequest();
            }

            var response = await _mediator.Send(new Queries.GetListNotifyParty.Request(page, pageSize));
            if (response.Count == 0)
            {
                return NotFound();
            }

            return errorResult ?? Ok(response);
        }

        /// <summary>
        /// Create Notify Party And Publish INotifyParty to Rabbit MQ.
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

        /// <summary>
        /// Update Notify Party And Publish INotifyParty to Rabbit MQ.
        /// </summary>
        /// <param name="notifyParty">Specified CargoOwnerId (int).</param>
        /// <returns>The List<see cref="INotifyParty"/>.</returns>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateAsync([FromBody] UpdateNotifyPartyCommand notifyParty)
        {
            var errorResult = CheckProblems();
            if (notifyParty.CargoOwnerId < 1 || notifyParty.NotifyAddress == null)
            {
                return BadRequest();
            }

            var result = await _mediator.Send(notifyParty);
            if (result == null)
            {
                return NotFound();
            }

            await _publishEndpoint.Publish<INotifyParty>(new { result.CargoOwnerId, result.NotifyAddress });
            return errorResult ?? Ok(result);
        }
    }
}
