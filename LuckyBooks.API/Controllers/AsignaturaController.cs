using Bussines;
using LoggerServices;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace LuckyBooks.API.Controllers
{
    [Route("api/Asignatura")]
    [ApiController]
    public class AsignaturaController : ControllerBase
    {
        private readonly AsignaturaService _asignaturaService;
        private readonly ILoggerManager _logger;


        public AsignaturaController(AsignaturaService asignaturaService , ILoggerManager logger)
        {
            _asignaturaService = asignaturaService;
            _logger = logger;
        }

        [HttpGet("{id}", Name = "asignaturaCreate")]
        public async Task<IActionResult> GetByIdAsignatura(int id)
        {

            var result = await _asignaturaService.GetById(id);

            if (result.codigoAsignatura == 0)
            {
                _logger.LogInfo($"Asignatura con id: {id} no existe en la base de datos");
                return NotFound();
            }
            return Ok(result);

        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsignatura()
        {
            var result = await _asignaturaService.GetAll();
            if (result.Count() == 0)
            {
                _logger.LogInfo($"No existe registro de Asignatura en la base de datos");
                return NotFound();
            }
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> CreateAsignatura([FromBody] AsignaturaForCreationDto asignaturaForCreationDto)
        {
            if (asignaturaForCreationDto == null)
            {
                _logger.LogError("El objeto AsignaturaForCreationDto enviado desde el cliente es nulo.");
                return BadRequest("No puede enviar Asignatura nulo.");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Estado de modelo no válido para el objeto AsignaturaForCreationDto");
                return UnprocessableEntity(ModelState);
            }
            var result = await _asignaturaService.Create(asignaturaForCreationDto);
            if (result.codigoAsignatura == 0)
            {
                _logger.LogError("La Asignatura contiene ID = 0");
                return BadRequest("Error al crear el Asignatura");
            }
            return CreatedAtRoute("asignaturaCreate", new { id = result.codigoAsignatura }, result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsignatura([FromBody] AsignaturaForUpdateDto asignaturaForUpdateDto)
        {
            if (asignaturaForUpdateDto == null)
            {
                _logger.LogError("Estado de modelo no válido para el objeto AsignaturaForUpdateDto");
                return BadRequest("No puede enviar un Asignatura nulo.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the AsignaturaForUpdateDto object");
                return BadRequest(ModelState);
            }

            var result = await _asignaturaService.GetById(asignaturaForUpdateDto.codigoAsignatura);

            if (result.codigoAsignatura == 0)
            {
                _logger.LogInfo($"Asignatura con id: {asignaturaForUpdateDto.codigoAsignatura} no existe en la base de datos");
                return NotFound();
            }

            _asignaturaService.Update(asignaturaForUpdateDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsignatura(int id)
        {
            var result = await _asignaturaService.GetById(id);

            if (result.codigoAsignatura == 0)
            {
                _logger.LogError($"Asignatura con id: {id} no existe en la base de datos");
                return NotFound();
            }
            _asignaturaService.Delete(result.codigoAsignatura);
            _logger.LogInfo($"Asignatura con id: {id} eliminado de la base de datos");
            return NoContent();
        }
    }
}
