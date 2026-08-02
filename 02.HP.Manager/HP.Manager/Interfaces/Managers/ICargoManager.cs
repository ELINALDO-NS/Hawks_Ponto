using HP.Manager.DTOs.Cargo;


namespace HP.Manager.Interfaces.Managers
{
    public interface ICargoManager
    {
        Task<CargoDto> AdicionarAsync(AdicionaCargoDto cargo, CancellationToken cancellationToken);
        Task<CargoDto> AtualizarAsync(AtualizaCargoDto cargo, CancellationToken cancellationToken);
        Task<bool> RemoverAsync(int id, CancellationToken cancellationToken);
        Task<CargoDto?> ObterPorIdAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<CargoDto>> ObterTodosAsync(CancellationToken cancellationToken);
    }
}
