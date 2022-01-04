using System;
using System.ComponentModel.DataAnnotations;

namespace CtlRestApi.Models
{
    public class TransferenciaDTO
    {
        public int BancoOrigenId { get; set; }
        public int CuentaOrigenId { get; set; }
        public int BancoDestinoId { get; set; }
        public int CuentaDestinoId { get; set; }
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
    }
}
