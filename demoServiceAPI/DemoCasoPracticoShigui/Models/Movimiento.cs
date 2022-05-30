using System;
using System.Collections.Generic;

#nullable disable

namespace DemoCasoPracticoShigui.Models
{
    public class Movimiento
    {
        public int MoIdMovimiento { get; set; }
        public string MoNumeroCuenta { get; set; }
        public DateTime MoFecha { get; set; }
        public string MoTipoMovimiento { get; set; }
        public decimal MoSaldoInicial { get; set; }
        public decimal MoMovimientos { get; set; }
        public decimal MoSaldoDisponible { get; set; }

        public virtual Cuenta MoNumeroCuentaNavigation { get; set; }
    }
}
