using CtlRestApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using CtlRestApi.Models;
using System;
using CtlRestApi.Services;
using System.Text.Json;

namespace CtlRestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BancosController : Controller
    {
        private readonly IBancoService _bancoService;
        private readonly ILogger<BancosController> _logger;

        public BancosController(IBancoService bancoService, ILogger<BancosController> logger)
        {
            _bancoService = bancoService;
            _logger = logger;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var bancosDTO = await _bancoService.Get();
                return Ok(bancosDTO);
            }
            catch (Exception ex)
            {
                var error = CrearErrorDTO(ex.Message);
                _logger.LogError(ex, JsonSerializer.Serialize(error));
                return BadRequest(error);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(BancoDTO bancoDTO)
        {
            try
            {
                var bancoDto = await _bancoService.Create(bancoDTO);
                return Ok(bancoDto);
            }
            catch (Exception ex)
            {
                var error = CrearErrorDTO(ex.Message);
                _logger.LogError(ex, JsonSerializer.Serialize(error));
                return BadRequest(error);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(BancoDTO bancoDTO)
        {
            try
            {
                var bancoDto = await _bancoService.Update(bancoDTO);
                return Ok(bancoDto);
            }
            catch (Exception ex)
            {
                var error = CrearErrorDTO(ex.Message);
                _logger.LogError(ex, JsonSerializer.Serialize(error));
                return BadRequest(error);
            }
        }

        [HttpDelete]
        public IActionResult Delete()
        {
            var error = CrearErrorDTO("Método no implementado");
            _logger.LogError(JsonSerializer.Serialize(error));
            return BadRequest(error);
        }

        private ErrorDTO CrearErrorDTO(string mensaje)
        {
            return new ErrorDTO()
            {
                Codigo = "ERROR",
                Mensaje = mensaje
            };
        }
    }
}
