using HP.Manager.DTOs.Pessoa;
using System;
using System.Collections.Generic;
using System.Text;

namespace HP.Manager.Interfaces.Managers
{
    public interface IPessoaManager
    {
        Task<PessoaDto> AdicionarAsync(AdicionaPessoaDto pessoa, CancellationToken cancellationToken);
        Task<PessoaDto?> AtualizarAsync(AtualizaPessoaDto pessoa, CancellationToken cancellationToken);
        Task<bool> RemoverAsync(int id, CancellationToken cancellationToken);
        Task<PessoaDto?> ObterPorIdAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<PessoaDto>> ObterTodosAsync(CancellationToken cancellationToken);

    }
}
