using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcPaginacionExamenMauricio.Models
{
    [Table("IMAGENESZAPASPRACTICA")]
    public class ImagenZapatilla
    {
        [Key]
        [Column("IDIMAGEN")]
        public int IdImagen { get; set; }
        [Column("IDPRODUCTO")]
        public int IdZapatilla { get; set; }
        [Column("IMAGEN")]
        public string Imagen { get; set; }
           
    }
}
