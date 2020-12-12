using System.ComponentModel.DataAnnotations.Schema;
namespace Bussines
{    
    public class AsignaturaForUpdateDto
    {
        public int codigoAsignatura { get; set; }
        public string descripcion { get; set; }
        public bool condicion { get; set; }
    }
}
