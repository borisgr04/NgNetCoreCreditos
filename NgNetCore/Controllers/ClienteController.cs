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

        [HttpGet]
        public IEnumerable<ClienteViewModel> Get()
        {
            var clientes = new List<ClienteViewModel>();
            clientes.Add(new ClienteViewModel { Identidad = "12233", NombreCompleto = "Andrea Pérez", Email = "q@a.com", Telefono = "31755533333" });
            clientes.Add(new ClienteViewModel { Identidad = "12255", NombreCompleto = "Pedro Pedroza", Email = "q@a.com", Telefono = "31855533333" });
            return clientes;
        }
    }
}