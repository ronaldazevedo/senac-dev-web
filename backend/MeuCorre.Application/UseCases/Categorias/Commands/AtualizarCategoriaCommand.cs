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
    public class AtualizarCategoriaCommand : IRequest <(string, bool)>
    {
           [Required(ErrorMessage = "Id da categoria é obrigatorio")]
           public required Guid CategoriaId { get; set; }
           [Required(ErrorMessage = "E necessário informar o id do usuário")]
           public required string Nome { get; set; }
           [Required(ErrorMessage = "Tipo (despesa ou receita) é obrigatorio!")]
           public required TipoTransacao Tipo { get; set; }
           public string? Descricao { get; set; }
           public string? Cor { get; set; }
           public string? Icone { get; set; }
    }
    
    internal class AtualizarCategoriaCommandHandler : IRequestHandler<AtualizarCategoriaCommand, (string, bool)>
    {
        private readonly ICategoriaRepository _categoriaRepository;
        public AtualizarCategoriaCommandHandler(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }
        public async Task<(string, bool)> Handle(AtualizarCategoriaCommand request, CancellationToken cancellationToken)

        {
           var categoria = await _categoriaRepository.ObterPorIdAsync(request.CategoriaId);
            if (categoria == null)
            {
                return ("Categoria não encontrada", false);
            }

            var categoriaEstaDuplicada = await _categoriaRepository.NomeExisteParaUsuarioAsync(request.Nome, request.Tipo, categoria.UsuarioId);

            if (categoriaEstaDuplicada)
            {
                return ("Já existe uma categoria com esses dados", false);
            }

            categoria.AtualizarInformacoes(
                request.Nome,
                request.Tipo,
                request.Descricao,
                request.Cor,
                request.Icone
                );

            await _categoriaRepository.AtualizarAsync(categoria);
            return ("Categoria atualizada com sucesso", true);

        }



    }

}
