using System.ComponentModel.DataAnnotations;

namespace CtlRestApi.Models
{
    public class BancoDTO
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        public BancoDTO(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
        }
    }
}
