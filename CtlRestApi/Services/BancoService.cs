using CtlRestApi.Data;
using CtlRestApi.Infrastructure.Exceptions;
using CtlRestApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CtlRestApi.Services
{
    public class BancoService : IBancoService
    {
        private readonly ApplicationContext _context;

        public BancoService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<List<BancoDTO>> Get()
        {
            return await _context.Bancos.Select(b => new BancoDTO(b.Id, b.Nombre)).ToListAsync(); ;
        }

        public async Task<BancoDTO> Create(BancoDTO bancoDTO)
        {
            var banco = new Banco
            {
                Id = bancoDTO.Id,
                Nombre = bancoDTO.Nombre,
                Cuentas = new List<Cuenta>()
            };
            _context.Bancos.Add(banco);
            await _context.SaveChangesAsync();
            return bancoDTO;
        }

        public async Task<BancoDTO> Update(BancoDTO bancoDTO)
        {
            var banco = await _context.Bancos.Where(b => b.Id == bancoDTO.Id).FirstOrDefaultAsync();
            if (banco == null)
            {
                throw new ErrorDeArgumentosException("Error al recuperar el registro del banco");
            }
            banco.Nombre = bancoDTO.Nombre;
            _context.Bancos.Update(banco);
            await _context.SaveChangesAsync();
            return bancoDTO;
        }

    }
}
