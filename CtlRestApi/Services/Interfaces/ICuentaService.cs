using CtlRestApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CtlRestApi.Services
{
    public interface ICuentaService
    {
        Task<Cuenta> Create(Cuenta cuenta);
        Task<List<Cuenta>> Get();
        Task<Cuenta> Update(Cuenta cuenta);
    }
}