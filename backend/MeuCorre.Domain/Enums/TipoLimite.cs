using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuCorre.Domain.Enums
{
    /// <summary>
    /// Define o tipo de limite aplicável a contas do tipo Cartão de Crédito.
    /// </summary>
    public enum TipoLimite
    {
        /// <summary>
        /// Limite total disponível no cartão.
        /// </summary>
        Total = 1,

        /// <summary>
        /// Limite mensal disponível para uso recorrente.
        /// </summary>
        Mensal = 2
    }
}
