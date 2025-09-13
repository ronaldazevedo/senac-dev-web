using MediatR;
using MeuCorre.Domain.Entities;
using MeuCorre.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuCorre.Application.UseCases.Usuarios.Commands
{
    public class AtualizarUsuarioCommand : IRequest<(string, bool)>
    {
        [Required(ErrorMessage = "Id do usuário é obrigatório")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        public required string Nome { get; set; }

        [Required(ErrorMessage = "Email é obrigatório")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Data de Nascimento é obrigatória")]
        public DateTime DataNascimento { get; set; }
    }

    internal class AtualizarUsuarioCommandHandler : IRequestHandler<AtualizarUsuarioCommand, (string, bool)>
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public AtualizarUsuarioCommandHandler(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<(string, bool)> Handle(AtualizarUsuarioCommand request, CancellationToken cancellationToken)
        {
            var usuarioExistente = await _usuarioRepository.ObterUsuarioPorEmail(request.Email);
            if (usuarioExistente == null || usuarioExistente.Id != request.Id)
            {
                return ("Usuário não encontrado.", false);
            }

            var ano = DateTime.Now.Year;
            var idade = ano - request.DataNascimento.Year;
            if (idade < 13)
            {
                return ("Usuário deve ser maior de 13 anos.", false);
            }

            var usuarioAtualizado = new Usuario(
            request.Nome,
            request.Email,
            request.DataNascimento,
            true
            );

            await _usuarioRepository.AtualizarUsuarioAsync(usuarioAtualizado);
            return ("Usuário atualizado com sucesso.", true);
        }
    }
}
