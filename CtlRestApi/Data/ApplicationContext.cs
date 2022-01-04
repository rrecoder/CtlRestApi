using CtlRestApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CtlRestApi.Data
{
    public class ApplicationContext: DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options): base(options)
        {
        }

        public DbSet<Banco> Bancos { get; set; }
        public DbSet<Cuenta> Cuentas { get; set;}
        public DbSet<Transferencia> Transferencias { get; set; }
    }
}
