using MeuCorre.Domain.Enums;

namespace MeuCorre.Domain.Entities
{
    public class Categoria : Entidade
    {
        public Guid UsuarioId { get; private set; }
        public string Nome { get; private set; }
        public string? Descricao  { get; private set; }
        public string? Cor { get; private set; }
        public string? Icone { get; private set; }
        public TipoTransacao TipoDaTransacao { get; private set; }
        public bool Ativo { get; private set; }

        // Propriedade de navegação para a entidade Usuario pois
        // o usuário pode ter várias categorias
        public virtual Usuario Usuario { get; private set; }

        public Categoria(Guid usuarioId, string nome, TipoTransacao tipoDaTransacao, string? descricao, string? cor, string? icone )
        {
            UsuarioId = usuarioId;
            Nome = nome.ToUpper();
            Descricao = descricao;
            Cor = cor;
            Icone = icone;
            TipoDaTransacao = tipoDaTransacao;
            Ativo = true;

        }

        public void AtualizarInformacoes(string nome, TipoTransacao tipoDaTransacao, bool ativo, string? descricao, string? cor, string? icone)
        {
            Nome = nome.ToUpper();
            Descricao = descricao;
            Cor = cor;
            Icone = icone;
            TipoDaTransacao = tipoDaTransacao;
            Ativo = ativo;
            AtualizarDataMoficacao();
        }

        public void Ativar()
        {
            Ativo = true;
            AtualizarDataMoficacao();
        }

        public void Inativar()
        {
            Ativo = false;
            AtualizarDataMoficacao();
        }
    }
}
