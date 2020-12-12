using Entity;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Data
{
    public class LibroRepository : ILibroRepository
    {
        public async Task<int> Create(Libro libro, SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand("Usp_Libro_Create", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@pDescripcion", libro.descripcion));
            cmd.Parameters.Add(new SqlParameter("@pAsignatura", libro.asignatura));
            cmd.Parameters.Add(new SqlParameter("@pStock", libro.stock));            
            cmd.Parameters.Add(new SqlParameter
            {
                ParameterName = "@pLibroID",
                Value = libro.id_libro,
                Direction = ParameterDirection.Output
            });

            try
            {
                await cmd.ExecuteNonQueryAsync();
                int id = (int)cmd.Parameters["@pLibroID"].Value;
                libro.id_libro = id;
            }
            catch (System.Exception)
            {

            }
            return libro.id_libro;
        }
        public bool Update(Libro libro, SqlConnection con)
        {
            bool exito = false;
            SqlCommand cmd = new SqlCommand("Usp_Libro_Update", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@pLibroID", libro.id_libro));
            cmd.Parameters.Add(new SqlParameter("@pDescripcion", libro.descripcion));
            cmd.Parameters.Add(new SqlParameter("@pAsignatura", libro.asignatura));
            cmd.Parameters.Add(new SqlParameter("@pStock", libro.stock));
            int nFilaAfectada = cmd.ExecuteNonQuery();
            exito = (nFilaAfectada > 0);
            return exito;
        }


        public bool Delete(int libroID, SqlConnection con)
        {
            bool exito = false;
            SqlCommand cmd = new SqlCommand("Usp_Libro_Delete", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@pLibroID", libroID));
            int nFilaAfectada = cmd.ExecuteNonQuery();
            exito = (nFilaAfectada > 0);
            return exito;
        }


        public async Task<Libro> GetById(int libroId, SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand("Usp_Libro_GetById", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@pLibroID", libroId));
            var libro = new Libro();
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleResult); //Lee el primer select, los demas ignora.

            if (reader != null)
            {
                while (await reader.ReadAsync())
                {
                    libro = LibroReader(reader);
                }
            }
            return libro;
        }

        public async Task<IEnumerable<Libro>> GetAll(SqlConnection con)
        {
            List<Libro> lstCliente = null;
            SqlCommand cmd = new SqlCommand("Usp_Libro_GetAll", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleResult); //Lee el primer select, los demas ignora.
            if (reader != null)
            {
                lstCliente = new List<Libro>();
                while (await reader.ReadAsync())
                {
                    lstCliente.Add(LibroReader(reader));
                }
            }
            return lstCliente;
        }

        private Libro LibroReader(SqlDataReader reader)
        {
            int posLibroID = reader.GetOrdinal("id_libro");
            int posDescripcion = reader.GetOrdinal("descripcion");
            int posAsignatura = reader.GetOrdinal("asignatura");
            int posStock = reader.GetOrdinal("stock");
            return new Libro()
            {
                id_libro = reader.GetInt32(posLibroID),
                descripcion = reader.GetString(posDescripcion),
                asignatura = reader.GetInt32(posAsignatura),
                stock = reader.GetInt32(posStock),          
            };
        }
    }
}
