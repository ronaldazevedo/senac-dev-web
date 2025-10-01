using MediatR;
using MeuCorre.Application.UseCases.Contas.Queries;
using Microsoft.AspNetCore.Mvc;

namespace MeuCorre.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContasController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> ListarContas([FromQuery] ListarContasQuery query)
        {
            var resultado = await _mediator.Send(query);
            return Ok(resultado);
        }
    }
}
