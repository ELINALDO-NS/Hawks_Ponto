using HP.Core.Entities;

namespace HP.Core.Interfaces.Repository
{
    public interface IEmpresaRepository
    {
       
        Task<Empresa> AdicionarAsync(Empresa empresa, CancellationToken cancellationToken);
        Task<Empresa?> AtualizarAsync(Empresa empresa, CancellationToken cancellationToken);
        Task<bool> RemoverAsync(int id, CancellationToken cancellationToken);        
        Task<Empresa?> ObterPorIdAsync(int id, CancellationToken cancellationToken);        
        Task<IEnumerable<Empresa>> ObterTodosAsync(CancellationToken cancellationToken);
        
    }
}
