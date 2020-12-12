using System.ComponentModel.DataAnnotations.Schema;
namespace Bussines
{
    public class AsignaturaForCreationDto
    {
        public int codigoAsignatura { get; set; }

        public string descripcion { get; set; }
        public bool condicion { get; set; }
    }
}
