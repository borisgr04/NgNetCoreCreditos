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
    //[Authorize("nombreRol")]
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
            if (request.ValorCredito < 1000001) //Este valor esta errado intensionalmente para que pueda verse la validación adicional desde el Front
            {
                ModelState.AddModelError("Valor Credito", "El valor del crédito debe ser menor a 100000");
                var problemDetails = new ValidationProblemDetails(ModelState)
                {
                    Status = StatusCodes.Status400BadRequest,
                };
                return BadRequest(problemDetails);
            }

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