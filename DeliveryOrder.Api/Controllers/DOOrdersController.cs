using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GoLogs.Events;
using GoLogs.Framework.Mvc;
using GoLogs.Interfaces;
using GoLogs.Services.DeliveryOrder.Api.Commands;
using GoLogs.Services.DeliveryOrder.Api.Infrastructure.Messaging.Events;
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
            var response = await Mediator.Send(new GoLogs.Services.DeliveryOrder.Api.Queries.GetById.Request { Id=id});
            return Ok(response);
        }
        /// <summary>
        /// Get DOOrder associated with the specified DO Order Number
        /// </summary>
        /// <param name="donumber"></param>
        /// <returns><strong> A Row data of DOOrder </strong>></returns>        
        [HttpGet]
        [Route("{donumber}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IDOOrder>> GetAsync(string donumber)
        {
            var errorResult = CheckProblems();
            var response = await Mediator.Send(new GoLogs.Services.DeliveryOrder.Api.Queries.GetByNumber.Request { DoNumber = donumber });
            return Ok(response);
        }
        /// <summary>
        /// Get List of DOOrder with paging
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <returns><strong>List of DOOrder</strong>></returns>        
        [HttpGet]
        [Route("{page:int}/{pagesize:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IList<IDOOrder>>> GetAsync(int page, int pagesize)
        {
            var errorResult = CheckProblems();
            var response = await Mediator.Send(new GoLogs.Services.DeliveryOrder.Api.Queries.GetList.Request { Page = page,PageSize = pagesize });
            return Ok(response);
        }
        /// <summary>
        /// Get List of DOOrder with paging and associated with the specified Cargo Owner Id
        /// </summary>
        /// <param name="cargoownerid"></param>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <returns><strong>List of DOOrder</strong>></returns>  
        [HttpGet]
        [Route("{cargoownerid:int}/{page:int}/{pagesize:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IList<IDOOrder>>> GetAsync(int cargoownerid,int page, int pagesize)
        {
            var errorResult = CheckProblems();
            var response = await Mediator.Send(new GoLogs.Services.DeliveryOrder.Api.Queries.GetListByCargoId.Request { CargoOwnerId = cargoownerid ,Page = page, PageSize = pagesize });
            return Ok(response);
        }
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
            var doorder = Mapper.Map<DOOrder>(doOrderInput);
            var errorResult = CheckProblems();            
            await Mediator.Send(doorder);       
            await PublishEndpoint.Publish<IDOOrder>(new { DODoOrderNumber = doorder.DoOrderNumber});             
            return errorResult ?? CreatedAtAction(Url.Action(nameof(GetAsync)), new { id = doorder.Id }, doorder);             
        }
    }
}