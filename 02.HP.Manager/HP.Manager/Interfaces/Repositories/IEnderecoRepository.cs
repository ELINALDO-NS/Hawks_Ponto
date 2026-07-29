using HP.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace HP.Manager.Interfaces.Repositories
{
    public interface IEnderecoRepository
    {
        Task<Endereco> AdicionarAsync(Endereco endereco, CancellationToken cancellationToken);
        Task<Endereco?> AtualizarAsync(Endereco endereco, CancellationToken cancellationToken);
        Task<bool> RemoverAsync(int id, CancellationToken cancellationToken);
        Task<Endereco?> ObterPorIdAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<Endereco>> ObterTodosAsync(CancellationToken cancellationToken);
    }
}
