using System.ComponentModel.DataAnnotations.Schema;
namespace Entity
{
    [Table("TB_Libro")]
   public class Libro
    {
        public int id_libro { get; set; }
        public string descripcion { get; set; }
        public int asignatura { get; set; }
        public int stock { get; set; }
    }
}
