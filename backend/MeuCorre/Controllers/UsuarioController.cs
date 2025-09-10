using MeuCorre.Application.UseCases.Usuarios.Commands;
using Microsoft.AspNetCore.Mvc;

namespace MeuCorre.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        ///<sumary>
        ///Cria um novo usuário
        ///<param name="command"<>/param>
        ///<sumary>
        [HttpPost]

        public async Task<IActionResult> CriarUsuario([FromBody] CriarUsuarioCommand command)
        {
           
        }
    }
}
