using HP.Manager.DTOs.Marcacao;

namespace HP.Manager.Interfaces
{
    public interface IMarcacaoManager
    {
        Task<MarcacaoDto> AdicionarAsync(AdicionaMarcacaoDto marcacao, CancellationToken cancellationToken);
        Task AdicionarMarcacoesEmLoteAsync(IEnumerable<MarcacaoDto> marcacoes, CancellationToken cancellationToken);
        Task<MarcacaoDto?> ObterPorIdAsync(long id, CancellationToken cancellationToken);
        Task<List<MarcacaoDto>> ObterPorCpfEPeriodoAsync(ObterPorCpfEPeriodoDto obterPorCpfEPeriodoDto, CancellationToken cancellationToken);
    }
}
