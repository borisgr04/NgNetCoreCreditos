using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NgNetCore.Models
{
    public class Cliente
    {
        [Key]
        public string Identificacion { get; set; }
        public string NombreCompleto { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public List<Credito> Creditos { get; set; }
    }
}