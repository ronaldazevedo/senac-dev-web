using MediatR;
using MeuCorre.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuCorre.Application.UseCases.Categorias.Commands
{
    public class DeletarCategoriaCommand : IRequest<(string, bool)>
    {
        [Required(ErrorMessage = "E necessário informar o id do usuário")]
        public required Guid UsuarioId { get; set; }
        [Required(ErrorMessage = "E necessário informar o id do categoria")]
        public required Guid CategoriaId { get; set; }

    }
    internal class DeletarCategoriaCommandHandler : IRequestHandler<DeletarCategoriaCommand, (string, bool)>
    {
        private readonly ICategoriaRepository _categoriaRepository;
        public DeletarCategoriaCommandHandler(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }
        public async Task<(string, bool)> Handle(DeletarCategoriaCommand request, CancellationToken cancellationToken)
        {
            var categoria = await _categoriaRepository.ObterPorIdAsync(request.CategoriaId);
            if (categoria == null)
            return ("Categoria não encontrada", false);

            if (categoria.UsuarioId != request.UsuarioId)
            return ("Categoria não pertence ao usuário informado", false);
           
            await _categoriaRepository.RemoverAsync(categoria);

             return ("Categoria removida com sucesso", true);
        }
    }
}
