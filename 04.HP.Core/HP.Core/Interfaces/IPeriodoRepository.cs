using HP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HP.Core.Interfaces
{
    public interface IPeriodoRepository
    {
        Task<Periodo> AdicionarAsync(Periodo periodo, CancellationToken cancellationToken );
        Task<Periodo> AtualizarAsync(Periodo periodo, CancellationToken cancellationToken );
        Task<Periodo?> FecharAsync(Periodo periodo, CancellationToken cancellationToken );
        Task<Periodo?> ReabrirAsync(Periodo periodo, CancellationToken cancellationToken );
        Task<bool> RemoverAsync(Periodo periodo, CancellationToken cancellationToken );
        Task<Periodo?> ObterPorIdAsync(int id, CancellationToken cancellationToken );
        Task<Periodo> ObterPeriodoAtualAsync(CancellationToken cancellationToken );
        Task<IEnumerable<Periodo>> ObterTodosAsync(CancellationToken cancellationToken );
    }
}
