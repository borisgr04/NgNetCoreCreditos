using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NgNetCore.Data;
using NgNetCore.Models;
using NgNetCore.ViewModels;

namespace NgNetCore.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ClienteController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<ClienteViewModel> Get()
        {
            return _context.Clientes.Select(c => new ClienteViewModel
            {
                Identificacion = c.Identificacion,
                Email = c.Email,
                NombreCompleto = c.NombreCompleto,
                Telefono = c.Telefono
            });
        }

        [HttpGet("{identificacion}")]
        public ClienteViewModel Get(string identificacion)
        {
            return _context.Clientes.Where(t=>t.Identificacion==identificacion).Select(c => new ClienteViewModel
            {
                Identificacion = c.Identificacion,
                Email = c.Email,
                NombreCompleto = c.NombreCompleto,
                Telefono = c.Telefono
            }).FirstOrDefault();
        }
    }
}