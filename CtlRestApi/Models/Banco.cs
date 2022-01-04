using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CtlRestApi.Models
{
    public class Banco
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<Cuenta> Cuentas { get; set;}
    }
}
