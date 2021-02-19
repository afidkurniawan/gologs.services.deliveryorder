// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GoLogs.Contracts.Events;
using GoLogs.Framework.Mvc;
using GoLogs.Interfaces;
using GoLogs.Services.DeliveryOrder.Api.Commands;
using GoLogs.Services.DeliveryOrder.Api.Models;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoLogs.Services.DeliveryOrder.Api.Controllers
{
    /// <summary>
    /// Public controller HistoriesController.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class HistoriesController : Controller
    {
        private readonly IProblemCollector _problemCollector;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMediator _mediator;

        public HistoriesController(IProblemCollector problemCollector, IMapper mapper,
           IPublishEndpoint publishEndpoint, IMediator mediator)
           : base(problemCollector, mapper, publishEndpoint, mediator)
        {
            _problemCollector = problemCollector;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
            _mediator = mediator;
        }

        /// <summary>
        /// Get List of History with paging and associated with the specified DOOrderNumber (optional).
        /// </summary>
        /// <param name="dOOrderNumber">Specified DOOrderNumber (string).</param>
        /// <param name="page">Specified Page (int).</param>
        /// <param name="pageSize">Specified PageSize (int).</param>
        /// <returns>The <see cref="IDOOrder"/>.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IList<HistoriesModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IList<IDOOrder>>> GetAsync([FromQuery] string dOOrderNumber, [FromQuery] int page, [FromQuery] int pageSize)
        {
            var errorResult = CheckProblems();
            IList<HistoriesModel> response;
            if (page < 0 || pageSize < 0)
            {
                return BadRequest();
            }

            if (dOOrderNumber != null)
            {
                response = await _mediator.Send(new Queries.GetHistoriesByDOOrderNumber.Request(dOOrderNumber, page, pageSize));
            }
            else
            {
                response = await _mediator.Send(new Queries.GetHistories.Request(page, pageSize));
            }

            if (response.Count == 0)
            {
                return NotFound();
            }

            return errorResult ?? Ok(response);
        }

        /// <summary>
        /// Create History And Publish Event Create to Rabbit MQ.
        /// </summary>
        /// <param name="request">Specified DONumber (string).</param>
        /// <returns>The <see cref="IHistory"/>.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult> CreateAsync([FromBody] CreateHistoryOrderCommand request)
        {
            var errorResult = CheckProblems();
            var result = await _mediator.Send(request);
            if (result == null)
            {
                return NotFound();
            }

            if (result.CurrentState == null)
            {
                return Conflict();
            }

            await _publishEndpoint.Publish<IHistoryCreatedEvent>(new { result.DOOrderNumber, result.CurrentState, result.EventStore });
            return errorResult ?? Ok(result);
        }
    }
}
