using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PosManagement.Application.Services.Commands.Manufacturer;
using PosManagement.Application.Services.Queries;
using PosManagement.Application.Services.Queries.ManufacturerQueries;

namespace PosManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManufacturerController : ControllerBase
    {
        private readonly IMediator mediator;

        public ManufacturerController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateManufacturer([FromBody] CreateManufacturer manufacturer, CancellationToken ct)
        {
            var result = await mediator.Send(manufacturer);
            if (result.IsFailure)
                return BadRequest(result.Errors);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync(CancellationToken ct)
        {
            var result = await mediator.Send(new GetAllManufacturers(), ct);
            if (result.IsFailure)
                return BadRequest(result.Errors);
            return Ok(result.Data);
        }
    }
}
