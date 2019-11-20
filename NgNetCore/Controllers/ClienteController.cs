using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NgNetCore.ViewModels;

namespace NgNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private List<ClienteViewModel> _clientes;
        public ClienteController()
        {
            _clientes = new List<ClienteViewModel>();
            _clientes.Add(new ClienteViewModel { Identificacion = "12233", NombreCompleto = "Andrea Pérez", Email = "q@a.com", Telefono = "31755533333" });
            _clientes.Add(new ClienteViewModel { Identificacion = "12255", NombreCompleto = "Pedro Pedroza", Email = "q@a.com", Telefono = "31855533333" });
        }

        [HttpGet]
        public IEnumerable<ClienteViewModel> Get()
        {
            return _clientes;
        }

        [HttpGet("{identificacion}")]
        public ClienteViewModel Get(string identificacion)
        {
            return _clientes.FirstOrDefault(t=>t.Identificacion== identificacion);
        }
    }
}