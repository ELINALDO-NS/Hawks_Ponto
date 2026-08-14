using HP.Manager.DTOs.Horario;


namespace HP.Manager.Interfaces
{
    public interface IHorarioManager
    {
        Task<HorarioDto> AdicionarAsync(AdicionaHorarioDto horariodto, CancellationToken cancellationToken);
        Task<HorarioDto> AtualizarAsync(AtualizaHorarioDto horariodto, CancellationToken cancellationToken);
        Task<bool> RemoverAsync(int id, CancellationToken cancellationToken);
        Task<HorarioDto?> ObterPorIdAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<HorarioDto>> ObterTodosAsync(CancellationToken cancellationToken);
    }
}
