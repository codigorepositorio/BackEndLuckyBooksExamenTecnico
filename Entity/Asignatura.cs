using System.ComponentModel.DataAnnotations.Schema;
namespace Entity
{
    [Table("TB_Asignatura")]
    public class Asignatura
    {
        public int id_asig { get; set; }
        public string descripcion { get; set; }
        public bool estado { get; set; }
    }
}
