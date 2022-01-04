using CtlRestApi.Infrastructure.Enums;
using System;

namespace CtlRestApi.Models
{
    public class Transferencia
    {
        public int Id { get; set; }
        public int CuentaIdOrigen { get; set; }
        public int CuentaIdDestino{ get; set;}
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }
        public EstadosTransferencias Estado { get; set; }
    }
}
