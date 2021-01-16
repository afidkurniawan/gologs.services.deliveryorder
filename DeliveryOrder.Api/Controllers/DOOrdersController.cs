using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GoLogs.Events;
using GoLogs.Framework.Mvc;
using GoLogs.Interfaces;
using GoLogs.Services.DeliveryOrder.Api.BusinessLogic;
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
        public DOOrdersController(IDOOrderLogic doOrderLogic, IProblemCollector problemCollector, IMapper mapper,
            IPublishEndpoint publishEndpoint,IMediator mediator)
            : base(doOrderLogic, problemCollector, mapper, publishEndpoint,mediator)
        {
        }
        /// <summary>
        /// Retrieving DOOrder using Id parameter of type int, has a single row return
        /// </summary>
        /// <param name="id"></param>
        /// <returns>IDOOrder</returns>        
        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IDOOrder>> GetAsync(int id)
        {            
            var errorResult = CheckProblems();
            var response = await _mediator.Send(new GoLogs.Services.DeliveryOrder.Api.Queries.GetById.Request { Id=id});
            return Ok(response);
        }
        /// <summary>
        /// Retrieving DOOrder using DoOrderNumber parameter of type string, has a single row return
        /// </summary>
        /// <param name="doordernumber"></param>
        /// <returns>IDOOrder</returns>        
        [HttpGet]
        [Route("{donumber}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IDOOrder>> GetAsync(string doordernumber)
        {
            var errorResult = CheckProblems();
            var response = await _mediator.Send(new GoLogs.Services.DeliveryOrder.Api.Queries.GetByNumber.Request { DoNumber = doordernumber });
            return Ok(response);
        }
        /// <summary>
        /// Retrieving List of DOOrder using page and pagesize parameter of type int,has a many row return
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{page:int}/{pagesize:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IList<IDOOrder>>> GetAsync(int page, int pagesize)
        {
            var errorResult = CheckProblems();
            var response = await _mediator.Send(new GoLogs.Services.DeliveryOrder.Api.Queries.GetList.Request { Page = page,PageSize = pagesize });
            return Ok(response);
        }
        /// <summary>
        /// Retrieving List of DOOrder using cargoownerid, page and pagesize parameter of type int,has a many row return
        /// </summary>
        /// <param name="cargoownerid"></param>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{cargoownerid:int}/{page:int}/{pagesize:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IList<IDOOrder>>> GetAsync(int cargoownerid,int page, int pagesize)
        {
            var errorResult = CheckProblems();
            var response = await _mediator.Send(new GoLogs.Services.DeliveryOrder.Api.Queries.GetListByCargoId.Request { CargoOwnerId = cargoownerid ,Page = page, PageSize = pagesize });
            return Ok(response);
        }
        /// <summary>
        /// Create DOOrder data, parameter that send only CargoOwnerId
        /// </summary>
        /// <param name="doOrderInput"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateAsync([FromBody] DOOrderDto doOrderInput)
        {
            var doorder = Mapper.Map<DOOrder>(doOrderInput);
            var errorResult = CheckProblems();            
            await _mediator.Send(doorder);
            return errorResult ?? CreatedAtAction(Url.Action(nameof(GetAsync)), new { id = doorder.Id }, doorder);             
        }
    }
}