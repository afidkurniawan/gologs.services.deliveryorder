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
            IPublishEndpoint publishEndpoint)
            : base(doOrderLogic, problemCollector, mapper, publishEndpoint)
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

            var doorder = await DOOrderLogic.GetDOOrderByIdAsync(id);
            if(doorder != null)
            {
                return Ok(doorder);
            }
            return NotFound();
        }
        /// <summary>
        /// Gets a DO Order by DoOrderNumber.
        /// </summary>
        /// <param name="donumber"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{donumber}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IDOOrder>> GetAllDOOrderByDoNumberAsync(string donumber)
        {
            if (donumber == null)
            {
                return BadRequest();
            }

            var doorder = await DOOrderLogic.GetAllDOOrderByDoNumberAsync(donumber);
            if (doorder != null)
            {
                
                return Ok();
                
            }
            return NotFound();
        }
        /// <summary>
        ///     Get Array of DOOrder
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DOOrder>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await DOOrderLogic.GetAllDOOrderAsync());
        }

        /// <summary>
        ///     Create a DOOrder
        /// </summary>
        /// <param name="doOrderInput"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateAsync([FromBody] DOOrderDto doOrderInput)
        {
            var doorder = Mapper.Map<DOOrder>(doOrderInput);
            await DOOrderLogic.CreateDOOrderAsync(doorder);
            var errorResult = CheckProblems();
            return errorResult ?? CreatedAtAction(
                Url.Action(nameof(GetAsync)), new { id = doorder.Id }, doorder);
        }
    }
}