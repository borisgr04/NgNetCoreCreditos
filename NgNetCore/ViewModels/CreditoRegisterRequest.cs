using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NgNetCore.ViewModels
{
    public class ClienteRegisterRequest
    {
        [Required]
        public string ClienteId { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
        [Required]
        public int NumeroCuotas { get; set; }
        [Required]
        public decimal ValorCredito { get; set; }

        [Required]
        public string Observacion { get; set; }
    }

}
