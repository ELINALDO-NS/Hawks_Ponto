using HP.Core.Entities;


namespace HP.Core.Interfaces
{
    public interface IMarcacaoRepository
    {
        Task<Marcacao> AdicionarAsync(Marcacao marcacao, CancellationToken cancellationToken);
        Task AdicionarMarcacoesEmLoteAsync(IEnumerable<Marcacao> marcacoes, CancellationToken cancellationToken);
        Task<Marcacao?> ObterPorIdAsync(long id, CancellationToken cancellationToken);
        Task<List<Marcacao>> ObterPorCpfEPeriodoAsync(string cpf, DateTimeOffset inicio, DateTimeOffset fim, CancellationToken cancellationToken);
        
    }
}
