using MediatR;
using MeuCorre.Domain.Entities;
using MeuCorre.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuCorre.Application.UseCases.Categorias.Commands
{
    public class CriarCategoriaCommad : IRequest <(string,bool)>
    {
        [Required (ErrorMessage = "E necessário informar o id do usuário")]
        public required Guid UsuarioId { get; set; }
        [Required(ErrorMessage = "Nome da categoria é obrigatório")]
        public required string Nome { get; set; }
        [Required(ErrorMessage = "Tipo da transação (ddespesa ou receita) é obrigatorio!")]
        public required TipoTransacao Tipo { get; set; }
        public string? Descricao { get; set; }
        public string? Cor { get; set; }
        public string? Icone { get; set; }

        
    }

    internal class CriarCategoriaCommadHandler : IRequestHandler<CriarCategoriaCommad, (string, bool)>
    {
        private readonly ICategoriaRepository _categoriaRepository;
        public CriarCategoriaCommadHandler(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }   
        public async Task<(string, bool)>Handle(CriarCategoriaCommad request, CancellationToken cancellationToken)
        {
          var existe = await _categoriaRepository.NomeExisteParaUsuarioAsync(request.Nome, request.Tipo, request.UsuarioId);
            if (existe)
            {
                return ("Categoria já cadastrada", false);
            }
            var novaCategoria = new Categoria(
            
                 request.UsuarioId,
                 request.Nome,
                 request.Tipo,
                 request.Descricao,
                 request.Cor,
                 request.Icone
                );
            await _categoriaRepository.AdicionarAsync(novaCategoria);
            return ("Categoria cadastrada com sucesso", true);

        }
    }

}
