using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstoqueSistema.Enums
{
    public enum TipoMovimentacao
    {
        [Description("Entrada")]
        Entrada = 1,
        [Description("Saida")]
        Saida = 2
    }
}
