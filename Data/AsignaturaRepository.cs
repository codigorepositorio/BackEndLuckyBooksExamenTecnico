using Entity;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Data
{
    public  class AsignaturaRepository : IAsignaturaRepository
    {
        public async Task<int> Create(Asignatura asignatura, SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand("Usp_Asignatura_Create", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@pDescripcion", asignatura.descripcion));
            cmd.Parameters.Add(new SqlParameter("@pEstado", asignatura.estado));            
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@pAsignaturaID",
                Value = asignatura.id_asig,
                Direction = ParameterDirection.Output
            });

            try
            {
                await cmd.ExecuteNonQueryAsync();
                int id = (int)cmd.Parameters["@pAsignaturaID"].Value;
                asignatura.id_asig = id;
            }
            catch (System.Exception)
            {

            }
            return asignatura.id_asig;
        }
        public bool Update(Asignatura asignatura, SqlConnection con)
        {
            bool exito = false;
            SqlCommand cmd = new SqlCommand("Usp_Asignatura_Update", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@pAsignaturaID", asignatura.id_asig));
            cmd.Parameters.Add(new SqlParameter("@pDescripcion", asignatura.descripcion));
            cmd.Parameters.Add(new SqlParameter("@pEstado", asignatura.estado));
            int nFilaAfectada = cmd.ExecuteNonQuery();
            exito = (nFilaAfectada > 0);
            return exito;
        }


        public bool Delete(int asignaturaId, SqlConnection con)
        {
            bool exito = false;
            SqlCommand cmd = new SqlCommand("Usp_Asignatura_Delete", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@pAsignaturaID", asignaturaId));
            int nFilaAfectada = cmd.ExecuteNonQuery();
            exito = (nFilaAfectada > 0);
            return exito;
        }


        public async Task<Asignatura> GetById(int libroId, SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand("Usp_Asignatura_GetById", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@pAsignaturaID", libroId));
            var Asignatura = new Asignatura();
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleResult); //Lee el primer select, los demas ignora.

            if (reader != null)
            {
                while (await reader.ReadAsync())
                {
                    Asignatura = LibroReader(reader);
                }
            }
            return Asignatura;
        }

        public async Task<IEnumerable<Asignatura>> GetAll(SqlConnection con)
        {
            List<Asignatura> lstCliente = null;
            SqlCommand cmd = new SqlCommand("Usp_Asignatura_GetAll", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleResult); //Lee el primer select, los demas ignora.
            if (reader != null)
            {
                lstCliente = new List<Asignatura>();
                while (await reader.ReadAsync())
                {
                    lstCliente.Add(LibroReader(reader));
                }
            }
            return lstCliente;
        }

        private Asignatura LibroReader(SqlDataReader reader)
        {
            int posLibroID = reader.GetOrdinal("id_asig");
            int posDescripcion = reader.GetOrdinal("descripcion");
            int posEstado = reader.GetOrdinal("estado");            
            return new Asignatura()
            {
                id_asig = reader.GetInt32(posLibroID),
                descripcion = reader.GetString(posDescripcion),
                estado = reader.GetBoolean(posEstado),                
            };
        }
    }
}

