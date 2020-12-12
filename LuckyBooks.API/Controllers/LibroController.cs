using Bussines;
using LoggerServices;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace LuckyBooks.API.Controllers
{
    [Route("api/libro")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly LibroService _libroService;
        private readonly ILoggerManager _logger;


        public ClienteController(LibroService libroService, ILoggerManager logger)
        {
            _libroService = libroService;
            _logger = logger;
        }

        [HttpGet("{id}", Name = "libroCreate")]
        public async Task<IActionResult> GetByIdLibro(int id)
        {

            var result = await _libroService.GetById(id);

            if (result.codigolibro == 0)
            {
                _logger.LogInfo($"Libro con id: {id} no existe en la base de datos");
                return NotFound();
            }
            return Ok(result);

        }

        [HttpGet]
        public async Task<IActionResult> GetAllLibro()
        {
            var result = await _libroService.GetAll();
            if (result.Count() == 0)
            {
                _logger.LogInfo($"No existe registro de clientes en la base de datos");
                return NotFound();
            }
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> CreateLibro([FromBody] LibroForCreationDto libroForCreationDto)
        {
            if (libroForCreationDto == null)
            {
                _logger.LogError("El objeto clienteForCreationDto enviado desde el Libro es nulo.");
                return BadRequest("No puede enviar un Libro nulo.");
            }
            if (!ModelState.IsValid)
            {
                _logger.LogError("Estado de modelo no válido para el objeto EmployeeForCreationDto");
                return UnprocessableEntity(ModelState);
            }
            var result = await _libroService.Create(libroForCreationDto);
            if (result.codigolibro == 0)
            {
                _logger.LogError("El Libro contiene ID = 0");
                return BadRequest("Error al crear el Libro");
            }            return CreatedAtRoute("libroCreate", new { id = result.codigolibro }, result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateLibro([FromBody] LibroForUpdateDto libroForUpdateDto)
        {
            if (libroForUpdateDto == null)
            {
                _logger.LogError("Estado de modelo no válido para el objeto clienteForUpdateDto");
                return BadRequest("No puede enviar un Libro nulo.");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the clienteForUpdateDto object");
                return BadRequest(ModelState);
            }

            var result = await _libroService.GetById(libroForUpdateDto.codigoLibro);

            if (result.codigolibro == 0)
            {
                _logger.LogInfo($"Libro con id: {libroForUpdateDto.codigoLibro} no existe en la base de datos");
                return NotFound();
            }

            _libroService.Update(libroForUpdateDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLibro(int id)
        {
            var result = await _libroService.GetById(id);

            if (result.codigolibro == 0)
            {
                _logger.LogError($"Libro con id: {id} no existe en la base de datos");
                return NotFound();
            }
            _libroService.Delete(result.codigolibro);
            _logger.LogInfo($"Libro con id: {id} eliminado de la base de datos");
            return NoContent();
        }
    }
}
