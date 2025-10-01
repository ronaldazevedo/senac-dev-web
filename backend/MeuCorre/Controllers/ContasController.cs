using MediatR;
using MeuCorre.Application.UseCases.Contas.Commands;
using MeuCorre.Application.UseCases.Contas.Queries;
using MeuCorre.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace MeuCorre.Controllers
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
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(ObterConta), new { id }, id);
        }

        [HttpGet]
        public async Task<IActionResult> ListarContas([FromQuery] TipoConta tipo, [FromQuery] bool? apenasAtivas, [FromQuery] string? ordenarPor)
        {
            var query = new ListarContasQuery
            {
                TipoConta = tipo,
                ApenasAtivas = apenasAtivas ?? false, 
                OrdenarPor = ordenarPor
            };

            var resultado = await _mediator.Send(query);
            return Ok(resultado);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterConta(Guid id, [FromQuery] Guid usuarioId)
        {
            var query = new ObterContaQuery { ContaId = id, UsuarioId = usuarioId };
            var resultado = await _mediator.Send(query);
            return Ok(resultado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarConta(Guid id, [FromBody] AtualizarContaCommand command)
        {
            if (id != command.ContaId)
                return BadRequest("ID da URL não corresponde ao ID da conta.");

            var resultado = await _mediator.Send(command);
            if (!resultado.sucesso)
                return Conflict(resultado.mensagem);

            return Ok(resultado.mensagem);
        }

        [HttpPatch("{id}/inativar")]
        public async Task<IActionResult> InativarConta(Guid id, [FromBody] InativarContaCommand command)
        {
            if (id != command.ContaId)
                return BadRequest("ID da URL não corresponde ao ID da conta.");

            var resultado = await _mediator.Send(command);
            if (!resultado.sucesso)
                return Conflict(resultado.mensagem);

            return Ok(resultado.mensagem);
        }

        [HttpPatch("{id}/reativar")]
        public async Task<IActionResult> ReativarConta(Guid id, [FromBody] ReativarContaCommand command)
        {
            if (id != command.ContaId)
                return BadRequest("ID da URL não corresponde ao ID da conta.");

            var resultado = await _mediator.Send(command);
            if (!resultado.sucesso)
                return Conflict(resultado.mensagem);

            return Ok(resultado.mensagem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirConta(Guid id, [FromQuery] Guid usuarioId, [FromQuery] bool confirmar)
        {
            var command = new ExcluirContaCommand
            {
                ContaId = id,
                UsuarioId = usuarioId,
                Confirmar = confirmar
            };

            var resultado = await _mediator.Send(command);
            if (!resultado.sucesso)
                return StatusCode(resultado.status, resultado.mensagem);

            return Ok(resultado.mensagem);
        }

        [HttpGet("saldo-consolidado")]
        public async Task<IActionResult> SaldoConsolidado([FromQuery] Guid usuarioId)
        {
            var query = new SaldoConsolidadoQuery { UsuarioId = usuarioId };
            var resultado = await _mediator.Send(query);
            return Ok(resultado);
        }
    }
}
