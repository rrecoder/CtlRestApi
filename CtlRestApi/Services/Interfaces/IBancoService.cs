using CtlRestApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CtlRestApi.Services
{
    public interface IBancoService
    {
        Task<BancoDTO> Create(BancoDTO bancoDTO);
        Task<List<BancoDTO>> Get();
        Task<BancoDTO> Update(BancoDTO bancoDTO);
    }
}