using HP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HP.Core.Interfaces
{
    public interface IDiaApontamentoRepository
    {
        Task AdicionarAsync(DiaApontamento diaApontamento, CancellationToken cancellationToken);
        Task AdicionarDiasAsync(IEnumerable<DiaApontamento> diaApontamento, CancellationToken cancellationToken);
        Task AtualizarAsync(DiaApontamento diaApontamento, CancellationToken cancellationToken);
        Task<bool> RemoverAsync(int PessoaId, IEnumerable<DateOnly> DiasDelete, CancellationToken cancellationToken);
        Task<DiaApontamento?> ObterPorIdAsync(long id, CancellationToken cancellationToken);
        Task<HashSet<DateOnly>?> ObterPorPessoaIdAsync(int PessoaId, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<DiaApontamento>> ObterPorPessoaEPeriodoAsync(int pessoaId, DateOnly dataInicio, DateOnly dataFim, CancellationToken cancellationToken);
    }
}
