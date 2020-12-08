using System.Threading.Tasks;
using GoLogs.Events;
using GoLogs.Framework.Mvc;
using GoLogs.Interfaces;
using GoLogs.Services.DeliveryOrder.Api.Commands;
using GoLogs.Services.DeliveryOrder.Api.Infrastructure.Messaging.Events;
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
        public DOOrdersController(IProblemCollector problemCollector, IMediator mediator)
            : base(problemCollector, mediator)
        {
        }

        /// <summary>
        ///     Gets a DO Order by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IDOOrder>> GetAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            //TODO
            
            return NotFound();
        }

        /// <summary>
        ///     Creates a new DO Order.
        /// </summary>
        /// <param name="createOrderCommand"></param>
        /// <returns></returns>
        /// <remarks>Publishes an <see cref="IDOOrderCreatedEvent"/> on successful creation.</remarks>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateAsync([FromBody] CreateOrderCommand createOrderCommand)
        {
            var commandResult = await Mediator.Send(createOrderCommand);
            
            if (commandResult < 0)
            {
                var errorResult = CheckProblems();
                if (errorResult != null)
                {
                    return errorResult;
                }

                return BadRequest();
            }

            await Mediator.Publish(new DOOrderCreatedEvent(createOrderCommand.DOOrder));

            return CreatedAtAction(Url.Action(nameof(GetAsync)), new {id = createOrderCommand.DOOrder.Id},
                createOrderCommand.DOOrder);
        }
    }
}