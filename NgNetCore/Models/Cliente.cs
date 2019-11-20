using System.ComponentModel.DataAnnotations;

namespace NgNetCore.Models
{
    public class Cliente
    {
        [Key]
        public string Identidad { get; set; }
        public string NombreCompleto { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
    }
}