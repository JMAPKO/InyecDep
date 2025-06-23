using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BACKEND02.Models
{
    public class Marca
    {
        [Key] //llave primaria
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //autoincremental
        public int IdMarca { get; set; }
        public string? NombreMarca { get; set; }
    }
}
