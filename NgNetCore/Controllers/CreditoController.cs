using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var cliente = _context.Clientes.Find(request.ClienteId);

            if (cliente == null)
            {
                ModelState.AddModelError("Cliente", "El Cliente no se encuentra registrado en el sistema");
                var problemDetails = new ValidationProblemDetails(ModelState)
                {
                    Status = StatusCodes.Status400BadRequest,
                };
                return BadRequest(problemDetails);
            }

            var credito = new Credito(cliente, request.Fecha, request.NumeroCuotas, request.ValorCredito);

            try
            {
                _context.Creditos.Add(credito);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Guardando Datos", "Se presento un inconveniente guardando los datos: " + ex.Message);
                var problemDetails = new ValidationProblemDetails(ModelState)
                {
                    Status = StatusCodes.Status400BadRequest,
                };
                return BadRequest(problemDetails);
            }

            return Ok(request);
        }


        [HttpPost("{creditoId}/abono")]
        public IActionResult PostAbonar(int creditoId, CreditoAbonoRequest request)
        {
            var credito = _context.Creditos.Include(t => t.Cuotas).FirstOrDefault(t => t.Id == creditoId);

            if (credito == null)
            {
                ModelState.AddModelError("Credito", "El Credito no se encuentra registrado en el sistema");
                var problemDetails = new ValidationProblemDetails(ModelState)
                {
                    Status = StatusCodes.Status400BadRequest,
                };
                return BadRequest(problemDetails);
            }
            credito.Abonar(request.NumeroCuota, request.Fecha, request.ValorAbono);
            try
            {
                _context.Creditos.Update(credito);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Guardando Datos", "Se presento un inconveniente guardando los datos: " + ex.Message);
                var problemDetails = new ValidationProblemDetails(ModelState)
                {
                    Status = StatusCodes.Status400BadRequest,
                };
                return BadRequest(problemDetails);
            }

            return Ok(request);
        }


        [HttpGet]
        public IEnumerable<CreditoViewModel> GetAll()
        {
            return _context.Creditos.Select(c =>
            new CreditoViewModel
            {
                Id = c.Id,
                IdentificacionCliente = c.ClienteId,
                Fecha = c.Fecha,
                NombreCliente = c.Cliente.NombreCompleto,
                NumeroCuotas = c.NumeroCuotas,
                ValorCredito = c.ValorCredito,

            }
            );
        }

        [HttpGet("{creditoId}/cuotas")]
        public IEnumerable<CuotaViewModel> GetCuotas(int creditoId)
        {
            return _context.Cuotas.Where(t => t.CreditoId == creditoId).Select(c =>
                new CuotaViewModel
                {
                    Id = c.Id,
                    Fecha = c.Fecha,
                    NumeroCuota = c.NumeroCuota,
                    ValorCuota = c.ValorCuota,
                    Abonado = c.Abonado,
                    FechaUltimoAbono = c.FechaUltimoAbono
                });
        }
    }
}