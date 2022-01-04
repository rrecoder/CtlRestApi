using CtlRestApi.Data;
using CtlRestApi.Infrastructure.Exceptions;
using CtlRestApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CtlRestApi.Services
{
    public class CuentaService : ICuentaService
    {
        private readonly ApplicationContext _context;

        public CuentaService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<List<Cuenta>> Get()
        {
            return await _context.Cuentas.ToListAsync(); ;
        }

        public async Task<Cuenta> Create(Cuenta cuenta)
        {
            _context.Cuentas.Add(cuenta);
            await _context.SaveChangesAsync();
            return cuenta;
        }

        public async Task<Cuenta> Update(Cuenta cuenta)
        {
            var cuentaExistente = await _context.Cuentas.Where(c => c.Id == cuenta.Id).FirstOrDefaultAsync();
            if (cuentaExistente == null)
            {
                throw new ErrorDeArgumentosException("Error al recuperar el registro de la cuenta");
            }
            if (cuentaExistente.BancoId != cuenta.BancoId)
            {
                throw new ErrorDeArgumentosException("No se puede actualizar el banco al cual pertenece una cuenta");
            }
            cuentaExistente.Numero = cuenta.Numero;
            cuentaExistente.SaldoInicial = cuenta.SaldoInicial;
            _context.Cuentas.Update(cuentaExistente);
            await _context.SaveChangesAsync();
            return cuentaExistente;
        }

    }
}
