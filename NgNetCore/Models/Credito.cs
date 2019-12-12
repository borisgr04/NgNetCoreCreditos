using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NgNetCore.Models
{
    public class Credito
    {
        //requerido por EF Core
        public Credito(){ }

        public Credito(Cliente cliente, DateTime fecha, int numeroCuotas,  decimal valorCredito)
        {
            
            Cliente = cliente;
            Fecha = fecha;
            NumeroCuotas = numeroCuotas;
            ValorCredito = valorCredito;

            for (int i = 0; i < NumeroCuotas; i++)
            {
                fecha = fecha.AddMonths(1);
                var cuota = new Cuota()
                {
                    CreditoId=Id, 
                    NumeroCuota = i+1,
                    Fecha = fecha,
                    ValorCuota = ValorCredito / NumeroCuotas
                };
                Cuotas.Add(cuota);
            }
        }

        public int Id { get; set; }
        public string ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public DateTime Fecha { get; set; }
        public int NumeroCuotas { get; set; }
        public decimal ValorCredito { get; set; }
        public List<Cuota> Cuotas { get; set; } = new List<Cuota>();

        public void Abonar(int numeroCuota,DateTime fechaAbono, decimal valorAbono) 
        {
            var cuota = Cuotas.FirstOrDefault(t => t.NumeroCuota == numeroCuota && t.Saldo>0);
            if (cuota != null)
            {
                if (cuota.Saldo == 0) 
                {
                    throw new Exception("La cuota a pagar ya se encuentra pagada");
                }
                cuota.Abonado += valorAbono;
                cuota.FechaUltimoAbono = fechaAbono;
            }
            else 
            {
              throw  new Exception("La cuota a pagar no se encuentra registrada"); 
            }
            
        }
    }

    public class Cuota
    {
        public int CreditoId { get; set; }
        public int Id { get; set; }
        public int NumeroCuota { get; set; }
        public DateTime Fecha { get; set; }
        public decimal ValorCuota { get; set; }
        public decimal Abonado { get; set; }
        public DateTime FechaUltimoAbono { get; set; }
        public decimal Saldo => ValorCuota - Abonado;
    }


}
