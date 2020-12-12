using Entity;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Data
{
    public  interface IAsignaturaRepository
    {
        Task<int> Create(Asignatura asignatura, SqlConnection con);
        Task<IEnumerable<Asignatura>> GetAll(SqlConnection con);
        Task<Asignatura> GetById(int asignaturaId, SqlConnection con);
        bool Update(Asignatura asignatura, SqlConnection con);
        bool Delete(int asignaturaId, SqlConnection con);

    }
}
