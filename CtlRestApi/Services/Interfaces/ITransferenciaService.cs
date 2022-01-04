using CtlRestApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CtlRestApi.Services
{
    public interface ITransferenciaService
    {
        Task<List<Transferencia>> Get();

        Task<Transferencia> Get(int id);

        Task<List<Transferencia>> GetByCuentaId(int cuentaId);

        Task<Transferencia> Create(TransferenciaDTO transferencia);

        Task<Transferencia> Update(int id);
    }
}