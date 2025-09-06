using MeuCorre.Domain.Enums;

namespace MeuCorre.Domain.Entities
{
    public class Categoria : Entidade
    {
        public Guid UsuarioId { get; private set; }
        public string Nome { get; private set; }
        public string Descricao  { get; private set; }
        public string Cor { get; private set; }
        public string Icone { get; private set; }
        public TipoTransacao TipoDaTransacao { get; private set; }

        // Propriedade de navegação para a entidade Usuario pois
        // o usuário pode ter várias categorias
        public virtual Usuario Usuario { get; private set; }

        public Categoria(Guid usuarioId, string nome, string descricao, string cor, string icone, TipoTransacao tipoDaTransacao)
        {
            UsuarioId = usuarioId;
            Nome = nome;
            Descricao = descricao;
            Cor = cor;
            Icone = icone;
            TipoDaTransacao = tipoDaTransacao;
        }
    }
}
