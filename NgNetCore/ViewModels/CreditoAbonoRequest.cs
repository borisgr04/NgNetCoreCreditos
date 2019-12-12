using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NgNetCore.ViewModels
{
    public class CreditoAbonoRequest
    {
        public DateTime Fecha { get; set; }
        public int NumeroCuota { get; set; }
        public decimal ValorAbono { get; set; }
    }
}
