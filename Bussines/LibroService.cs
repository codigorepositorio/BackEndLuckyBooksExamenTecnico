using AutoMapper;
using Data;
using Entity;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Bussines
{
    public class LibroService : ConexionGeneral
    {
        private readonly ILibroRepository _libroRepository;
        private readonly IMapper _mapper;
        public LibroService(ILibroRepository libroRepository, IMapper mapper)
        {
            _libroRepository = libroRepository;
            _mapper = mapper;
        }
        public async Task<LibroDto> GetById(int clienteID)
        {
            LibroDto libro = null;
            using (SqlConnection con = new SqlConnection(CadenaConexion))
            {
                await con.OpenAsync();
                var result = await _libroRepository.GetById(clienteID, con);
                libro = _mapper.Map<LibroDto>(result);
            }
            return libro;
        }

        public async Task<IEnumerable<LibroDto>> GetAll()
        {
            IEnumerable<LibroDto> lstCliente = null;
            using (SqlConnection con = new SqlConnection(CadenaConexion))
            {
                await con.OpenAsync();
                var result = await _libroRepository.GetAll(con);
                lstCliente = _mapper.Map<IEnumerable<LibroDto>>(result);
            }
            return lstCliente;
        }

        public async Task<LibroForCreationDto> Create(LibroForCreationDto libroForCreationDto)
        {
            using (SqlConnection con = new SqlConnection(CadenaConexion))
            {
                await con.OpenAsync();
                var libroEntity = _mapper.Map<Libro>(libroForCreationDto);
                var libroIdReturn = await _libroRepository.Create(libroEntity, con);
                libroForCreationDto.codigolibro = libroIdReturn;
            }
            return libroForCreationDto;
        }

        public bool Update(LibroForUpdateDto clienteForUpdateDto)
        {
            bool exito = false;
            using (SqlConnection con = new SqlConnection(CadenaConexion))
            {
                con.Open();
                var clienteEntity = _mapper.Map<Libro>(clienteForUpdateDto);
                exito = _libroRepository.Update(clienteEntity, con);
            }
            return exito;
        }

        public bool Delete(int libroID)
        {
            bool exito = false;
            using (SqlConnection con = new SqlConnection(CadenaConexion))
            {
                con.Open();
                exito = _libroRepository.Delete(libroID, con);
            }
            return exito;
        }
    }
}

