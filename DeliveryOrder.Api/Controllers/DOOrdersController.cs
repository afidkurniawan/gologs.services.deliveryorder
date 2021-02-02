﻿// -------------------------------------------------------------
// Copyright Go-Logs. All rights reserved.
// Proprietary and confidential.
// Unauthorized copying of this file is strictly prohibited.
// -------------------------------------------------------------
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GoLogs.Framework.Mvc;
using GoLogs.Interfaces;
using GoLogs.Services.DeliveryOrder.Api.Models;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoLogs.Services.DeliveryOrder.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]

    // ReSharper disable once InconsistentNaming
    public class DOOrdersController : Controller
    {
        public DOOrdersController(IProblemCollector problemCollector, IMapper mapper,
            IPublishEndpoint publishEndpoint, IMediator mediator)
            : base(problemCollector, mapper, publishEndpoint, mediator)
        {
        }
        /// <summary>        
        /// Get DOOrder associated with the specified ID
        /// </summary>
        /// <param name="id">The identifier of the DO Order.</param>
        /// <returns>The <see cref="IDOOrder"/>.</returns>
        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(typeof(DOOrder),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IDOOrder>> GetAsync(int id)
        {            
            var errorResult = CheckProblems();
            var response = await Mediator.Send(new Queries.GetById.Request(id));
            if (response == null) return NotFound();
            return errorResult ?? Ok(response);
        }
        /// <summary>
        /// Get DOOrder associated with the specified DO Order Number
        /// </summary>
        /// <param name="doNumber">The identifier of the DO Order.</param>        
        /// <returns>The <see cref="IDOOrder"/>.</returns>      
        [HttpGet]
        [Route("{doNumber}")]
        [ProducesResponseType(typeof(DOOrder), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IDOOrder>> GetAsync(string doNumber)
        {
            var errorResult = CheckProblems();
            var response = await Mediator.Send(new Queries.GetByNumber.Request(doNumber));
            if (response == null) return NotFound();
            return errorResult ?? Ok(response);
        }
        /// <summary>
        /// Get List of DOOrder with paging and associated with the specified Cargo Owner Id (optional)
        /// </summary>
        /// <param name="cargoOwnerId"></param>          
        /// <param name="page"></param>                                
        /// <param name="pageSize"></param>                               
        /// <returns>The <see cref="IDOOrder"/>.</returns> 
        [HttpGet]
        [ProducesResponseType(typeof(IList<DOOrder>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IList<IDOOrder>>> GetAsync([FromQuery] int? cargoOwnerId,[FromQuery] int page, [FromQuery] int pageSize)
        {
            var errorResult = CheckProblems();
            IList<DOOrder> response;
            if (page < 0 || pageSize < 0) return BadRequest();
            if (cargoOwnerId != null)
            {
                response = await Mediator.Send(new Queries.GetListByCargoOwnerId.Request((int)cargoOwnerId, page, pageSize));
            }
            else
            {
                response = await Mediator.Send(new Queries.GetList.Request(page, pageSize));
            }
            if (response.Count == 0) return NotFound();
            return errorResult ?? Ok(response);                        
        }
        /// <summary>
        /// Create DOOrder data, parameter that send only CargoOwnerId And Publish DOOrderCreatedEvent to Rabbit MQ
        /// </summary>
        /// <param name="doOrderInput"></param>
        /// <returns>The <see cref="IDOOrder"/>.</returns> 
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateAsync([FromBody] DOOrderDto doOrderInput)
        {
            var doOrder = Mapper.Map<DOOrder>(doOrderInput);
            var errorResult = CheckProblems();            
            await Mediator.Send(doOrder);       
            await PublishEndpoint.Publish<IDOOrder>(new { doOrder.DOOrderNumber, doOrder.CargoOwnerId});             
            return errorResult ?? CreatedAtAction(Url.Action(nameof(GetAsync)), new { id = doOrder.Id }, doOrder);             

        }
    }
}
