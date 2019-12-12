using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NgNetCore.ViewModels
{
    public class CreditoViewModel
    {
        public int Id { get; set; }
        public string IdentificacionCliente { get; set; }
        public string NombreCliente { get; set; }
        public DateTime Fecha { get; set; }
        public int NumeroCuotas { get; set; }
        public decimal ValorCredito { get; set; }
        public string Observacion { get; set; }
    }

    public class CuotaViewModel
    {
        public int Id { get; set; }
        public int NumeroCuota { get; set; }
        public DateTime Fecha { get; set; }
        public decimal ValorCuota { get; set; }
        public decimal Abonado { get; set; }
        public DateTime FechaUltimoAbono { get; set; }
        public decimal Saldo => ValorCuota - Abonado;
    }
}
