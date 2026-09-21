using HP.Core.Entities;
using HP.Manager.DTOs.Periodo;


namespace HP.Manager.Interfaces
{
    public interface IPeriodoManager
    {
        Task<PeriodoDto> AdicionarAsync(PeriodoDto periodo, CancellationToken cancellationToken);
        Task<PeriodoDto> AtualizarAsync(PeriodoDto periodo, CancellationToken cancellationToken);
        Task<PeriodoDto?> FecharAsync(FechaPeriodoDto fechaPeriodoDto, CancellationToken cancellationToken);
        Task<PeriodoDto?> ReabrirAsync(ReabriPeridoDto periodo, CancellationToken cancellationToken);
        Task<bool> RemoverAsync(int id, CancellationToken cancellationToken);
        Task<PeriodoDto?> ObterPorIdAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<PeriodoDto>?> ObterTodosAsync(CancellationToken cancellationToken);
    }
}
