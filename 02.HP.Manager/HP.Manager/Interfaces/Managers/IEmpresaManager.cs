using HP.Core;
using HP.Manager.DTOs.Empresa;
using System;
using System.Collections.Generic;
using System.Text;

namespace HP.Manager.Interfaces.Managers
{
    public interface IEmpresaManager
    {
        Task<EmpresaDto> AdicionarAsync(AdicionaEmpresaDto empresa, CancellationToken cancellationToken);
        Task<EmpresaDto> AtualizarAsync(AtualizaEmpresaDto empresa, CancellationToken cancellationToken);
        Task<bool> RemoverAsync(int id, CancellationToken cancellationToken);
        Task<EmpresaDto?> ObterPorIdAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<EmpresaDto>> ObterTodosAsync(CancellationToken cancellationToken);
    }
}
