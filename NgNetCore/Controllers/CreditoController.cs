using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NgNetCore.Data;
using NgNetCore.Models;
using NgNetCore.ViewModels;

namespace NgNetCore.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CreditoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public CreditoController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Post(ClienteRegisterViewModel request)
        {
            var credito = new Credito
            {
                ClienteId = request.ClienteId,
                NumeroCuotas = request.NumeroCuotas,
                ValorCredito = request.ValorCredito,
                Fecha= request.Fecha
            };
            int i = 1;
            foreach (var item in request.Cuotas)
            {
                var cuota = new Cuota()
                {
                    NumeroCuota = i,
                    Fecha = item.Fecha,
                    ValorCuota = item.ValorCuota
                };
                i++;
                credito.Cuotas.Add(cuota);
            }
            //_context.Creditos.Add(credito);
            //_context.SaveChanges();
            return Ok(request);   
        }
    }
}