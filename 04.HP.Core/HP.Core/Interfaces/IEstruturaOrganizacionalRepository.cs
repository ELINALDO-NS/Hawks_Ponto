using HP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HP.Core.Interfaces
{
    public interface IEstruturaOrganizacionalRepository
    {
        Task<EstruturaOrganizacional> AdicionarAsync(EstruturaOrganizacional estruturaOrganizacional, CancellationToken cancellationToken);
        Task<EstruturaOrganizacional?> AtualizarAsync(EstruturaOrganizacional estruturaOrganizacional, CancellationToken cancellationToken);
        Task<bool> RemoverAsync(int id, CancellationToken cancellationToken);
        Task<EstruturaOrganizacional?> ObterPorIdAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<EstruturaOrganizacional>> ObterTodosAsync(CancellationToken cancellationToken);
    }
}
