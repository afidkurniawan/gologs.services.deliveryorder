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
    public class HistoriesController : Controller
    {
        public HistoriesController(IProblemCollector problemCollector, IMapper mapper,
          IPublishEndpoint publishEndpoint, IMediator mediator)
          : base(problemCollector, mapper, publishEndpoint, mediator)
        {
        }
        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IHistory>> GetAsync(int id)
        {
            var errorResult = CheckProblems();
            var response = await Mediator.Send(new GoLogs.Services.DeliveryOrder.Api.Queries.GetHistoryById.Request { Id = id });
            return Ok(response);
        }
        [HttpGet]
        //[Route("{page:int}/{pagesize:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IList<IHistory>>> GetAsync(int page, int pagesize)
        {
            var errorResult = CheckProblems();
            var response = await Mediator.Send(new GoLogs.Services.DeliveryOrder.Api.Queries.GetHistoryList.Request { Page = page, PageSize = pagesize });
            return Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateAsync([FromBody] HistoryDto historyInput)
        {
            var history = Mapper.Map<History>(historyInput);
            var errorResult = CheckProblems();
            await Mediator.Send(history);
            await PublishEndpoint.Publish<IHistory>(new { DoNumber = history.DoNumber, StateId = history.StateId }); // 1            
            return errorResult ?? CreatedAtAction(Url.Action(nameof(GetAsync)), new { id = history.Id }, history);
        }
    }
}
