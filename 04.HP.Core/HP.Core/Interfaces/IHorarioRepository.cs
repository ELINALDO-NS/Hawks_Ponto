using HP.Core.Entities;


namespace HP.Core.Interfaces
{
    public interface IHorarioRepository
    {
        Task<Horario> AdicionarAsync(Horario horario, CancellationToken cancellationToken);
        Task<Horario?> AtualizarAsync(Horario horario, CancellationToken cancellationToken);
        Task<bool> RemoverAsync(int id, CancellationToken cancellationToken);
        Task<Horario?> ObterPorIdAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<Horario>> ObterTodosAsync(CancellationToken cancellationToken);
    }
}
