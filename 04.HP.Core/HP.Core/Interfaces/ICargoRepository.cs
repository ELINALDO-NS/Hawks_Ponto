using HP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HP.Core.Interfaces
{
    public interface ICargoRepository
    {
        Task<Cargo> AdicionarAsync(Cargo cargo, CancellationToken cancellationToken);
        Task<Cargo?> AtualizarAsync(Cargo cargo, CancellationToken cancellationToken);
        Task<bool> RemoverAsync(int id, CancellationToken cancellationToken);
        Task<Cargo?> ObterPorIdAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<Cargo>> ObterTodosAsync(CancellationToken cancellationToken);
    }
}
