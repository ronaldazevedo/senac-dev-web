using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuCorre.Domain.Enums
{
    // <summary>
    /// Representa os tipos de conta disponíveis no sistema.
    /// </summary>
        
    public enum TipoConta
    {
        /// <summary>
        /// Conta do tipo carteira, usada para armazenar dinheiro físico ou valores fora do sistema bancário.
        /// </summary>
        Carteira = 1,

        /// <summary>
        /// Conta bancária tradicional, vinculada a uma instituição financeira.
        /// </summary>
        ContaBancaria = 2,

        /// <summary>
        /// Conta de cartão de crédito, usada para registrar gastos e limites de crédito.
        /// </summary>
        CartaoCredito = 3
    }
}

