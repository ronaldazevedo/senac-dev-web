using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuCorre.Domain.Enums
{
    /// <summary>
    /// Enum que representa os tipos de limite disponíveis para contas do tipo cartão de crédito.
    /// </summary>
    public enum TipoLimite
    {
        /// <summary>
        /// Limite total disponível.
        /// </summary>
        Total = 1,

        /// <summary>
        /// Limite mensal disponível.
        /// </summary>
        Mensal = 2
    }

}
