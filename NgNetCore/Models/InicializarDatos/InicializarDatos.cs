using NgNetCore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NgNetCore.Models.InicializarDatos
{
    public class InicializarDatos
    {
        private readonly ApplicationDbContext _context;
        public InicializarDatos(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public void InicializarCliente()
        {
            if (!_context.Clientes.Any())
            {
                var clientes = new List<Cliente>
                {
                    new Cliente { Identificacion = "12233", NombreCompleto = "Andrea Pérez", Email = "q@a.com", Telefono = "31755533333" },
                    new Cliente { Identificacion = "12255", NombreCompleto = "Pedro Pedroza", Email = "q@a.com", Telefono = "31855533333" }
                };
                _context.AddRange(clientes);
                _context.SaveChanges();
            }
            
        }
    }
}
