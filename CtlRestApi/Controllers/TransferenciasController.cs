using CtlRestApi.Data;
using CtlRestApi.Infrastructure.Enums;
using CtlRestApi.Infrastructure.Exceptions;
using CtlRestApi.Models;
using CtlRestApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace CtlRestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransferenciasController : Controller
    {        
        private readonly ITransferenciaService _transferenciaService;
        private readonly ILogger<TransferenciasController> _logger;

        public TransferenciasController(ITransferenciaService transferenciaService, ILogger<TransferenciasController> logger)
        {
            _transferenciaService = transferenciaService;
            _logger = logger;            
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var transferencias = await _transferenciaService.Get();
                return Ok(transferencias);
            }
            catch (Exception ex)
            {
                var error = CrearErrorDTO(ex.Message);
                _logger.LogError(ex, JsonSerializer.Serialize(error));
                return BadRequest(error);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var transferencia = await _transferenciaService.Get(id);
                return Ok(transferencia);
            }
            catch (Exception ex)
            {
                var error = CrearErrorDTO(ex.Message);
                _logger.LogError(ex, JsonSerializer.Serialize(error));
                return BadRequest(error);
            }
        }

        [HttpGet("cuenta/{idCuenta}")]
        public async Task<IActionResult> GetByCuentaId(int idCuenta)
        {
            try
            {
                var transferencias = await _transferenciaService.GetByCuentaId(idCuenta);
                return Ok(transferencias);
            }
            catch (Exception ex)
            {
                var error = CrearErrorDTO(ex.Message);
                _logger.LogError(ex, JsonSerializer.Serialize(error));
                return BadRequest(error);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(TransferenciaDTO transferencia)
        {
            try
            {
                var nuevaTransferencia = await _transferenciaService.Create(transferencia);
                return Ok(nuevaTransferencia);
            }
            catch (Exception ex)
            {                
                var error = CrearErrorDTO(ex.Message);
                _logger.LogError(ex, JsonSerializer.Serialize(error));
                return BadRequest(error);
            }
        }

        [HttpPut("{id}/{estado}")]
        public async Task<IActionResult> Put(int id, EstadosTransferencias estado)
        {
            try
            {
                var transferenciaActualizada = await _transferenciaService.Update(id, estado);
                return Ok(transferenciaActualizada);
            }
            catch (Exception ex)
            {
                var error = CrearErrorDTO(ex.Message);
                _logger.LogError(ex, JsonSerializer.Serialize(error));
                return BadRequest(error);
            }
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
