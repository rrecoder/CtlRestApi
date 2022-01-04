using CtlRestApi.Infrastructure.Enums;
using CtlRestApi.Models;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CtlRestApi.Data
{
    public class Seeder
    {
        private readonly ApplicationContext _context;
        private readonly IWebHostEnvironment _env;

        public Seeder(ApplicationContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task SeedAsync()
        {
            _context.Database.EnsureCreated();

            // Registros de bancos y cuentas
            var banco1 = new Banco
            {
                Id =  1,
                Nombre = "CONTINENTAL",
                Cuentas = new List<Cuenta>
                {
                    new Cuenta {Id = 1, Numero = "1111111", SaldoInicial = 1500000, BancoId = 1},
                    new Cuenta {Id = 2, Numero = "2222222", SaldoInicial = 500000, BancoId = 1}
                }
            };

            var banco2 = new Banco
            {
                Id = 2,
                Nombre = "BNF",
                Cuentas = new List<Cuenta>
                {
                    new Cuenta {Id = 3, Numero = "333333", SaldoInicial = 1000000, BancoId = 2},
                    new Cuenta {Id = 4, Numero = "444444", SaldoInicial = 1500000, BancoId = 2}
                }
            };

            var banco3 = new Banco
            {
                Id = 3,
                Nombre = "ITAU",
                Cuentas = new List<Cuenta>
                {
                    new Cuenta {Id = 5, Numero = "555555", SaldoInicial = 1750000, BancoId = 3},
                    new Cuenta {Id = 6, Numero = "666666", SaldoInicial = 1600000, BancoId = 3}
                }
            };

            // Registros de transferencias
            var transferencia1 = new Transferencia
            {
                Id = 1,
                CuentaIdOrigen = 1,
                CuentaIdDestino = 3,
                Monto = 50000,
                Fecha = System.DateTime.Now,
                Estado = EstadosTransferencias.Aceptado
            };

            var transferencia2 = new Transferencia
            {
                Id = 2,
                CuentaIdOrigen = 4,
                CuentaIdDestino = 2,
                Monto = 65000,
                Fecha = System.DateTime.Now,
                Estado = EstadosTransferencias.Aceptado
            };

            var transferencia3 = new Transferencia
            {
                Id = 3,
                CuentaIdOrigen = 5,
                CuentaIdDestino = 1,
                Monto = 850000,
                Fecha = System.DateTime.Now,
                Estado = EstadosTransferencias.Aceptado
            };

            var transferencia4 = new Transferencia
            {
                Id = 4,
                CuentaIdOrigen = 6,
                CuentaIdDestino = 2,
                Monto = 450000,
                Fecha = System.DateTime.Now,
                Estado = EstadosTransferencias.Aceptado
            };

            _context.Bancos.AddRange(banco1, banco2, banco3);
            _context.Transferencias.AddRange(transferencia1, transferencia2, transferencia3, transferencia4);

            await _context.SaveChangesAsync();
        }
    }
}
