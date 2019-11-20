using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NgNetCore.Models
{
    public class Credito
    {
        public int Id { get; set; }
        public string ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public DateTime Fecha { get; set; }
        public int NumeroCuotas { get; set; }
        public decimal Valor { get; set; }
        public decimal ValorCredito { get; set; }
        public List<Cuota> Cuotas { get; set; } = new List<Cuota>();
    }

    public class Cuota
    {
        public int Id { get; set; }
        public int NumeroCuota { get; set; }
        public DateTime Fecha { get; set; }
        public decimal ValorCuota { get; set; }
        public decimal SaldoCuota { get; set; }
    }
}
