using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticaMvcCore2Iniciales.Models
{
    [Table("VISTAPEDIDOS")]
    public class VistaPedido
    {
        [Key]
        [Column("IDVISTAPEDIDOS")]
        public int IdVistaPedidos { get; set; }

        [Column("IdUsuario")]
        public int IdUsuario { get; set; }

        [Column("Nombre")]
        public string NombreUsuario { get; set; }

        [Column("Apellidos")]
        public string ApellidosUsuario { get; set; }

        [Column("Titulo")]
        public string TituloLibro { get; set; }

        [Column("Precio")]
        public int PrecioLibro { get; set; }

        [Column("Portada")]
        public string PortadaLibro { get; set; }

        [Column("FECHA")]
        public DateTime FechaPedido { get; set; }

        [Column("PRECIOFINAL")]
        public int PrecioFinal { get; set; }
        }
    }
