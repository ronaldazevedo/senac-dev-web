using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuCorre.Domain.Enums
{
    /// <summary>
    /// Enum que representa os tipos de conta disponíveis no sistema.
    /// </summary>
    public enum TipoConta
    {
        /// <summary>
        /// Conta do tipo carteira, usada para armazenar dinheiro físico ou digital.
        /// </summary>
        Carteira = 1,

        /// <summary>
        /// Conta bancária tradicional vinculada a uma instituição financeira.
        /// </summary>
        ContaBancaria = 2,

        /// <summary>
        /// Conta de cartão de crédito utilizada para compras parceladas ou à vista.
        /// </summary>
        CartaoCredito = 3,
        Cartao = 4
    }
}
