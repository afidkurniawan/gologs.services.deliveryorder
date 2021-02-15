// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
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
    /// Public controller DOOrdersController.
    /// </summary>
    [ApiController]
    [Route("[controller]")]

    // ReSharper disable once InconsistentNaming
    public class DOOrdersController : Controller
    {
        private readonly IProblemCollector _problemCollector;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="DOOrdersController"/> class.
        /// </summary>
        /// <param name="problemCollector">Define IProblemCollector.</param>
        /// <param name="mapper">Define IMapper.</param>
        /// <param name="publishEndpoint">Define IPublishEndpoint.</param>
        /// <param name="mediator">Define IMediator.</param>
        public DOOrdersController(IProblemCollector problemCollector, IMapper mapper,
            IPublishEndpoint publishEndpoint, IMediator mediator)
            : base(problemCollector, mapper, publishEndpoint, mediator)
        {
            _problemCollector = problemCollector;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
            _mediator = mediator;
        }

        /// <summary>
        /// Get DOOrder associated with the specified ID.
        /// </summary>
        /// <param name="id">Specified Id (int).</param>
        /// <returns>The <see cref="IDOOrder"/>.</returns>
        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(DOOrder), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IDOOrder>> GetAsync(int id)
        {
            var errorResult = CheckProblems();
            var response = await _mediator.Send(new Queries.GetById.Request(id));
            if (response == null)
            {
                return NotFound();
            }

            return errorResult ?? Ok(response);
        }

        /// <summary>
        /// Get DOOrder associated with the specified DO Order Number.
        /// </summary>
        /// <param name="doNumber">Specified DO Number (string).</param>
        /// <returns>The <see cref="IDOOrder"/>.</returns>
        [HttpGet]
        [Route("{doNumber}")]
        [ProducesResponseType(typeof(DOOrder), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IDOOrder>> GetAsync(string doNumber)
        {
            var errorResult = CheckProblems();
            var response = await _mediator.Send(new Queries.GetByNumber.Request(doNumber));
            if (response == null)
            {
                return NotFound();
            }

            return errorResult ?? Ok(response);
        }

        /// <summary>
        /// Get List of DOOrder with paging and associated with the specified Cargo Owner Id (optional).
        /// </summary>
        /// <param name="cargoOwnerId">Specified CargoOwnerId (int).</param>
        /// <param name="page">Specified Page (int).</param>
        /// <param name="pageSize">Specified PageSize (int).</param>
        /// <returns>The <see cref="IDOOrder"/>.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IList<DOOrder>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IList<IDOOrder>>> GetAsync([FromQuery] int? cargoOwnerId, [FromQuery] int page, [FromQuery] int pageSize)
        {
            var errorResult = CheckProblems();
            IList<DOOrder> response;
            if (page < 0 || pageSize < 0)
            {
                return BadRequest();
            }

            if (cargoOwnerId != null)
            {
                response = await _mediator.Send(new Queries.GetListByCargoOwnerId.Request((int)cargoOwnerId, page, pageSize));
            }
            else
            {
                response = await _mediator.Send(new Queries.GetList.Request(page, pageSize));
            }

            if (response.Count == 0)
            {
                return NotFound();
            }

            return errorResult ?? Ok(response);
        }

        /// <summary>
        /// Create DOOrder And Publish DOOrderCreatedEvent to Rabbit MQ.
        /// </summary>
        /// <param name="doOrderInput">Specified CargoOwnerId (int).</param>
        /// <returns>The <see cref="IDOOrder"/>.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateAsync([FromBody] CreateOrderCommand doOrderInput)
        {
            var errorResult = CheckProblems();
            var result = await _mediator.Send(doOrderInput);
            await _publishEndpoint.Publish<IDOOrder>(new { result.DOOrderNumber, result.CargoOwnerId });
            return errorResult ?? CreatedAtAction(Url.Action(nameof(GetAsync)), new { id = result.Id }, result);
        }
    }
}
