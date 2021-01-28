using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GoLogs.Framework.Mvc;
using GoLogs.Interfaces;
using GoLogs.Services.DeliveryOrder.Api.Models;
using Queries = GoLogs.Services.DeliveryOrder.Api.Queries;
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
        /// <param name="id"></param>
        /// <returns><strong> A Row data of DOOrder </strong>></returns>        
        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IDOOrder>> GetAsync(int id)
        {            
            var errorResult = CheckProblems();
            var response = await Mediator.Send(new Queries.GetById.Request { Id=id});            
            return errorResult ?? Ok(response);
        }
        /// <summary>
        /// Get DOOrder associated with the specified DO Order Number
        /// </summary>
        /// <param name="doNumber"></param>        
        /// <returns><strong> A Row data of DOOrder </strong>></returns>        
        [HttpGet]
        [Route("{doNumber}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IDOOrder>> GetAsync(string doNumber)
        {
            var errorResult = CheckProblems();
            var response = await Mediator.Send(new Queries.GetByNumber.Request { DoNumber = doNumber });            
            return errorResult ?? Ok(response);
        }
        /// <summary>
        /// Get List of DOOrder with paging
        /// </summary>
        /// <param name="request"></param>        
        /// <returns><strong>List of DOOrder</strong>></returns>        
        [HttpGet]
        [Route("DOOrders/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IList<IDOOrder>>> GetAsync([FromQuery] Queries.GetList.Request request)
        {
            var errorResult = CheckProblems();
            var response = await Mediator.Send(request);            
            return errorResult ?? Ok(response);
        }
        /// <summary>
        /// Get List of DOOrder with paging and associated with the specified Cargo Owner Id
        /// </summary>
        /// <param name="request"></param>        
        /// <returns><strong>List of DOOrder</strong>></returns>  
        [HttpGet]
        [Route("DOOrdersByCargoOwnerId/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IList<IDOOrder>>> GetAsync([FromQuery] Queries.GetListByCargoOwnerId.Request request)
        {
            var errorResult = CheckProblems();
            var response = await Mediator.Send(request);            
            return errorResult ?? Ok(response);
        }
        /*
        /// <summary>
        /// Get List of DOOrder with paging and associated with the specified Cargo Owner Id
        /// </summary>
        /// <param name="doOrderInput"></param>        
        /// <returns><strong>List of DOOrder</strong>></returns>  
        [HttpGet]
        [Route("{cargoownerid:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IList<IDOOrder>>> GetAsync(DOOrderDto doOrderInput)
        {
            var errorResult = CheckProblems();
            var response = await Mediator.Send(new Queries.GetByCargoOwnerId.Request { CargoOwnerId = doOrderInput.CargoOwnerId});
            if (response == null) return NotFound();
            return errorResult ?? Ok(response);
        }
        */
        /// <summary>
        /// Create DOOrder data, parameter that send only CargoOwnerId And Publish DOOrderCreatedEvent to Rabbit MQ
        /// </summary>
        /// <param name="doOrderInput"></param>
        /// <returns>A row the data you just created</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateAsync([FromBody] DOOrderDto doOrderInput)
        {
            var doOrder = Mapper.Map<DOOrder>(doOrderInput);
            var errorResult = CheckProblems();            
            await Mediator.Send(doOrder);       
            await PublishEndpoint.Publish<IDOOrder>(new { DODoOrderNumber = doOrder.DOOrderNumber, CargoOwnerId = doOrder.CargoOwnerId});             
            return errorResult ?? CreatedAtAction(Url.Action(nameof(GetAsync)), new { id = doOrder.Id }, doOrder);             
        }
    }
}