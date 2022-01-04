using System.ComponentModel.DataAnnotations;

namespace CtlRestApi.Models
{
    public class Cuenta
    {
        public int Id { get; set; }
        [Required]
        public string Numero { get; set; }
        public decimal SaldoInicial { get; set; } = 0;
        public int BancoId { get; set; }
    }
}
