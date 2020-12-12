using Entity;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Data
{
    public  interface ILibroRepository
    {
        Task<int> Create(Libro libro, SqlConnection con);
        Task<IEnumerable<Libro>> GetAll(SqlConnection con);
        Task<Libro> GetById(int libroId, SqlConnection con);
        bool Update(Libro libro, SqlConnection con);
        bool Delete(int libroID, SqlConnection con);

    }
}
