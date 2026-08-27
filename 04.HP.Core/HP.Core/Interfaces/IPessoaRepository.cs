using HP.Core.Entities;

namespace HP.Core.Interfaces
{
    public interface IPessoaRepository
    {
        Task<Pessoa> AdicionarAsync(Pessoa pessoa, CancellationToken cancellationToken);
        Task<Pessoa?> AtualizarAsync(Pessoa pessoa, CancellationToken cancellationToken);
        Task<bool> RemoverAsync(int id, CancellationToken cancellationToken);
        Task<Pessoa?> ObterPorIdAsync(int id, CancellationToken cancellationToken);
        Task<Pessoa?> ObterPorMatriculaAsync(int Matricula, CancellationToken cancellationToken);
        Task<IEnumerable<Pessoa>> ObterTodosAsync(CancellationToken cancellationToken);
    }
}
