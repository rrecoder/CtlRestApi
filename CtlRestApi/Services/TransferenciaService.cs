using CtlRestApi.Data;
using CtlRestApi.Infrastructure.Enums;
using CtlRestApi.Infrastructure.Exceptions;
using CtlRestApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CtlRestApi.Services
{
    public class TransferenciaService : ITransferenciaService
    {
        private readonly ApplicationContext _context;

        public TransferenciaService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<List<Transferencia>> Get()
        {
            return await _context.Transferencias.ToListAsync();
        }

        public async Task<Transferencia> Get(int id)
        {
            return await _context.Transferencias.
                Where(t => t.Id == id).
                FirstOrDefaultAsync();
        }

        public async Task<List<Transferencia>> GetByCuentaId(int cuentaId)
        {
            return await _context.Transferencias.
                Where(t => t.CuentaIdOrigen == cuentaId || t.CuentaIdDestino == cuentaId).
                ToListAsync();
        }

        public async Task<Transferencia> Create(TransferenciaDTO transferencia)
        {
            // Validaciones de datos de la Transferencia
            // Validar que el Banco Ordenante y beneficiario no sean los mismos
            if (transferencia.BancoOrigenId == transferencia.BancoDestinoId)
            {
                throw new BancosDiferentesException("Bancos no deben ser iguales");
            }

            // Validar que la Cuenta Ordenante y beneficiario no sean los mismos
            if (transferencia.CuentaOrigenId == transferencia.CuentaDestinoId)
            {
                throw new CuentasDiferentesException("Cuentas no deben ser iguales");
            }

            // Validar monto de transferencia > 0
            if (transferencia.Monto <= 0)
            {
                throw new ErrorDeArgumentosException("El monto de la transferencia debe ser mayor que cero");
            }

            // Recuperar registro de Banco Origen
            var bancoOrigen = await _context.Bancos.Where(b => b.Id == transferencia.BancoOrigenId).FirstOrDefaultAsync();
            if (bancoOrigen == null)
            {
                throw new ErrorDeArgumentosException("No se encuentra el registro del banco de origen");
            }

            // Recuperar registro de Banco Destino
            var bancoDestino = await _context.Bancos.Where(b => b.Id == transferencia.BancoDestinoId).FirstOrDefaultAsync();
            if (bancoOrigen == null)
            {
                throw new ErrorDeArgumentosException("No se encuentra el registro del banco de destino");
            }

            // Recuperar registro de Cuenta Origen
            var cuentaOrigen = await _context.Cuentas.Where(c => c.Id == transferencia.CuentaOrigenId).FirstOrDefaultAsync();
            if (cuentaOrigen == null)
            {
                throw new ErrorDeArgumentosException("No se encuentra el registro de la cuenta de origen");
            }

            // Recuperar registro de Cuenta Destino
            var cuentaDestino = await _context.Cuentas.Where(c => c.Id == transferencia.CuentaDestinoId).FirstOrDefaultAsync();
            if (cuentaDestino == null)
            {
                throw new ErrorDeArgumentosException("No se encuentra el registro de la cuenta de destino");
            }

            // Verificar si banco origen y cuenta origen corresponden
            if (bancoOrigen.Id != cuentaOrigen.BancoId)
            {
                throw new ErrorDeArgumentosException("La cuenta de origen no pertenece al banco de origen");
            }

            // Verificar si banco destino y cuenta destino corresponden
            if (bancoDestino.Id != cuentaDestino.BancoId)
            {
                throw new ErrorDeArgumentosException("La cuenta de destino no pertenece al banco de destino");
            }

            // Verificar si la cuenta de Origen tiene saldo suficiente
            // Sumar las transferencias realizadas a la cuenta de origen si hubieren + el saldo inicial de la cuenta
            var saldoIngresos = await _context.Transferencias.
                Where(c => c.CuentaIdDestino == cuentaOrigen.Id && c.Estado == EstadosTransferencias.Aceptado).
                SumAsync(s => s.Monto);

            var saldoEgresos = await _context.Transferencias.
                Where(c => c.CuentaIdOrigen == cuentaOrigen.Id && c.Estado == EstadosTransferencias.Aceptado).
                SumAsync(s => s.Monto);

            decimal saldoFinal = cuentaOrigen.SaldoInicial + saldoIngresos - saldoEgresos;

            if (saldoFinal < transferencia.Monto)
            {
                throw new FondoInsuficienteException("Insuficiencia de fondos en la cuenta origen");
            }

            // Crear registro de la transferencia
            int newId = _context.Transferencias.Count();
            newId++;
            var nuevaTransferencia = new Transferencia
            {
                Id = newId,
                CuentaIdOrigen = cuentaOrigen.Id,
                CuentaIdDestino = cuentaDestino.Id,
                Monto = transferencia.Monto,
                Fecha = transferencia.Fecha,
                Estado = EstadosTransferencias.Pendiente
            };
            _context.Transferencias.Add(nuevaTransferencia);
            await _context.SaveChangesAsync();
            return nuevaTransferencia;
        }

        public async Task<Transferencia> Update(int id)
        {
            var transferencia = await _context.Transferencias.FindAsync(id);
            if (transferencia == null)
            {
                throw new ErrorDeArgumentosException($"No se encuentra la transferencia con ID: {id}");
            }
            transferencia.Estado = EstadosTransferencias.Aceptado;
            _context.Transferencias.Update(transferencia);
            await _context.SaveChangesAsync();
            return transferencia;
        }
    }
}
