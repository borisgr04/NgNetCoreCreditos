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
        public IActionResult Post(ClienteRegisterRequest request)
        {
            
            if (request.ValorCredito < 1000001) //Este valor esta errado intensionalmente para que pueda verse la validación adicional desde el Front
            {
                ModelState.AddModelError("Valor Crédito", "El valor del crédito debe ser menor a $100.000");
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
            var fecha = request.Fecha;
            for (int i=0; i< request.NumeroCuotas; i++)
            {
                fecha = fecha.AddMonths(1);
                var cuota = new Cuota()
                {
                    NumeroCuota = i,
                    Fecha = fecha,
                    ValorCuota = request.ValorCredito/ request.NumeroCuotas
                };
                credito.Cuotas.Add(cuota);
            }
            //por manejo de la complejidad no estan try-catch pero deben ir
            //_context.Creditos.Add(credito);
            //_context.SaveChanges();

            return Ok(request);   
        }
    }
}