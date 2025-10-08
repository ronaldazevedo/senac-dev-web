using MediatR;
using Microsoft.AspNetCore.Mvc;
using MeuCorre.Application.UseCases.Contas.Commands;
using MeuCorre.Application.UseCases.Contas.Queries;
using MeuCorre.Domain.Enums;
using Raven.Client.Exceptions;

namespace MeuCorre.API.Controllers
{
    [ApiController]
    [Route("api/v1/contas")]
    public class ContasController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CriarConta([FromBody] CriarContaCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(ObterConta), new { id = result.Id }, result);
        }

        [HttpGet]
        public async Task<IActionResult> ListarContas(
            [FromQuery] Guid usuarioId,
            [FromQuery] TipoConta? tipo,
            [FromQuery] bool apenasAtivas = true,
            [FromQuery] string? ordenarPor = null)
        {
            var query = new ListarContasQuery
            {
                UsuarioId = usuarioId,
                Tipo = tipo,
                ApenasAtivas = apenasAtivas,
                OrdenarPor = ordenarPor
            };

            var contas = await _mediator.Send(query);
            return Ok(contas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterConta([FromRoute] Guid id)
        {
            var conta = await _mediator.Send(new ObterContaQuery { ContaId = id });

            if (conta == null)
                return NotFound();

            return Ok(conta);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarConta([FromRoute] Guid id, [FromBody] AtualizarContaCommand command)
        {
            if (id != command.ContaId)
                return BadRequest("ID da conta não confere com o corpo da requisição.");

            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPatch("{id}/inativar")]
        public async Task<IActionResult> InativarConta([FromRoute] Guid id, [FromQuery] Guid usuarioId)
        {
            await _mediator.Send(new InativarContaCommand { ContaId = id, UsuarioId = usuarioId });
            return NoContent();
        }

        [HttpPatch("{id}/reativar")]
        public async Task<IActionResult> ReativarConta([FromRoute] Guid id, [FromQuery] Guid usuarioId)
        {
            await _mediator.Send(new ReativarContaCommand { ContaId = id, UsuarioId = usuarioId });
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirConta(
            [FromRoute] Guid id,
            [FromQuery] Guid usuarioId,
            [FromQuery] bool confirmar)
        {
            try
            {
                await _mediator.Send(new ExcluirContaCommand
                {
                    ContaId = id,
                    UsuarioId = usuarioId,
                    Confirmar = confirmar
                });

                return NoContent();
            }
            catch (ConflictException ex)
            {
                return Conflict(new { erro = ex.Message });
            }
        }

        [HttpGet("saldo-consolidado")]
        [ProducesResponseType(typeof(object), 200)]
        public async Task<IActionResult> SaldoConsolidado([FromQuery] Guid usuarioId)
        {
            var saldo = await _mediator.Send(new CalcularSaldoTotalQuery { UsuarioId = usuarioId });
            return Ok(new { saldo });
        }
    }
}
