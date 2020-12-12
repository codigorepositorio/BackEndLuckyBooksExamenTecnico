using AutoMapper;
using Data;
using Entity;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Bussines
{
    public class AsignaturaService : ConexionGeneral
    {
        private readonly IAsignaturaRepository _asignaturaRepository;
        private readonly IMapper _mapper;
        public AsignaturaService(IAsignaturaRepository asignaturaRepository, IMapper mapper)
        {
            _asignaturaRepository = asignaturaRepository;
            _mapper = mapper;
        }
        public async Task<AsignaturaDto> GetById(int clienteID)
        {
            AsignaturaDto asignatura = null;
            using (SqlConnection con = new SqlConnection(CadenaConexion))
            {
                await con.OpenAsync();
                var result = await _asignaturaRepository.GetById(clienteID, con);
                asignatura = _mapper.Map<AsignaturaDto>(result);
            }
            return asignatura;
        }

        public async Task<IEnumerable<AsignaturaDto>> GetAll()
        {
            IEnumerable<AsignaturaDto> lstAsignatura = null;
            using (SqlConnection con = new SqlConnection(CadenaConexion))
            {
                await con.OpenAsync();
                var result = await _asignaturaRepository.GetAll(con);
                lstAsignatura = _mapper.Map<IEnumerable<AsignaturaDto>>(result);
            }
            return lstAsignatura;
        }

        public async Task<AsignaturaForCreationDto> Create(AsignaturaForCreationDto libroForCreationDto)
        {
            using (SqlConnection con = new SqlConnection(CadenaConexion))
            {
                await con.OpenAsync();
                var asignaturaEntity = _mapper.Map<Asignatura>(libroForCreationDto);
                var asignaturaIdReturn = await _asignaturaRepository.Create(asignaturaEntity, con);
                libroForCreationDto.codigoAsignatura = asignaturaIdReturn;
            }
            return libroForCreationDto;
        }

        public bool Update(AsignaturaForUpdateDto asignaturaForUpdateDto )
        {
            bool exito = false;
            using (SqlConnection con = new SqlConnection(CadenaConexion))
            {
                con.Open();
                var asignaturaEntity = _mapper.Map<Asignatura>(asignaturaForUpdateDto);
                exito = _asignaturaRepository.Update(asignaturaEntity, con);
            }
            return exito;
        }

        public bool Delete(int asignaturaId)
        {
            bool exito = false;
            using (SqlConnection con = new SqlConnection(CadenaConexion))
            {
                con.Open();
                exito = _asignaturaRepository.Delete(asignaturaId, con);
            }
            return exito;
        }
    }
}

