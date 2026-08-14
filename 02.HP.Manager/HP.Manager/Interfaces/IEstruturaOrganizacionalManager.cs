using HP.Manager.DTOs.EstruturaOrganizacional;


namespace HP.Manager.Interfaces
{
    public interface IEstruturaOrganizacionalManager
    {
        Task<EstruturaOrganizacionalDto> AdicionarAsync(AdicionaEstruturaOrganizacionalDto estruturaOrganizacional, CancellationToken cancellationToken);
        Task<EstruturaOrganizacionalDto> AtualizarAsync(AtualizaEstruturaOrganizacionalDto estruturaOrganizacional, CancellationToken cancellationToken);
        Task<bool> RemoverAsync(int id, CancellationToken cancellationToken);
        Task<EstruturaOrganizacionalDto?> ObterPorIdAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<EstruturaOrganizacionalDto>> ObterTodosAsync(CancellationToken cancellationToken);
    }
}
