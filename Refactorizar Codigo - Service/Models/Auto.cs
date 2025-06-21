using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BACKEND02.Models
{
    public class Auto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // autoincremental
        public int IdAuto { get; set; }
        public string? NombreAuto { get; set; }
        public int IdMarca { get; set; }

        [Column(TypeName ="decimal (18,2)")]
        public decimal Precio { get; set; }

        [ForeignKey("IdMarca")]
        public virtual Marca Marca { get; set; } // Relación con la entidad Marca

    }
}
