using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuCorre.Application.UseCases.Usuarios.Commands
{
    ///<sumary>
    ///Comando para criar um novo usuário
    ///Aqui você pode adicionar propriedades necessárias para criar o usuário, como Nome, Email, Senha, etc.
    ///<sumary>
    public class CriarUsuarioCommand : IRequest<string>
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public DateTime DataNascimento { get; set; }

        public CriarUsuarioCommand(string nome, string email, string senha)
        {
            Nome = nome;
            Email = email;
            Senha = senha;
        }   
    }
}
