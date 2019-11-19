using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NgNetCore.ViewModels
{
    public class ClienteRegisterViewModel
    {
        [Required]
        public int ClienteId { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
        [Required]
        public int NumeroCuotas { get; set; }
        [Required]
        public decimal ValorCredito { get; set; }

        [Required]
        public string Observacion { get; set; }
        public List<CuotaRegisterViewModel> Cuotas { get; set; } = new List<CuotaRegisterViewModel>();
    }

    public class CuotaRegisterViewModel
    {
        public DateTime Fecha { get; set; }
        public decimal ValorCuota { get; set; }
    }
}
