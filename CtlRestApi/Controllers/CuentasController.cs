using CtlRestApi.Data;
using CtlRestApi.Models;
using CtlRestApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace CtlRestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CuentasController : Controller
    {
        private readonly ICuentaService _cuentaService;
        private readonly ILogger<BancosController> _logger;

        public CuentasController(ICuentaService cuentaService, ILogger<BancosController> logger)
        {
            _cuentaService = cuentaService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var cuentas = await _cuentaService.Get();
                return Ok(cuentas);
            }
            catch (Exception ex)
            {
                var error = CrearErrorDTO(ex.Message);
                _logger.LogError(ex, JsonSerializer.Serialize(error));
                return BadRequest(error);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Cuenta cuenta)
        {
            try
            {
                var nuevaCuenta = await _cuentaService.Create(cuenta);
                return Ok(nuevaCuenta);
            }
            catch (Exception ex)
            {
                var error = CrearErrorDTO(ex.Message);
                _logger.LogError(ex, JsonSerializer.Serialize(error));
                return BadRequest(error);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(Cuenta cuenta)
        {
            try
            {
                var cuentaActualizada = await _cuentaService.Update(cuenta);
                return Ok(cuentaActualizada);
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
