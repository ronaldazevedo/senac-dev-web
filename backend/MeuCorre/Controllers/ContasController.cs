using MediatR;
using MeuCorre.Application.UseCases.Contas.Commands;
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirConta(Guid id, [FromBody] ExcluirContaCommand command)
        {
            if (id != command.ContaId)
                return BadRequest("ID da URL não corresponde ao ID da conta.");

            var resultado = await _mediator.Send(command);
            return StatusCode(resultado.status, resultado.mensagem);
        }
    }
}
