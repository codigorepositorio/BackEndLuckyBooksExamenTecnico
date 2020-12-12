using System.ComponentModel.DataAnnotations.Schema;
namespace Bussines
{    
   public class LibroForUpdateDto
    {
        public int codigoLibro { get; set; }
        public string descripcion { get; set; }
        public int asignatura { get; set; }
        public int stock { get; set; }
    }
}
