using HP.Core.Entities;
using HP.Core.Interfaces;
using HP.Manager.DTOs.Marcacao;
using HP.Manager.Interfaces;
using MapsterMapper;


namespace HP.Manager.Implementation
{
    public class MarcacaoManager(IMarcacaoRepository _repository, IMapper _mapper) : IMarcacaoManager
    {
        public async Task<MarcacaoDto> AdicionarAsync(AdicionaMarcacaoDto marcacao, CancellationToken cancellationToken)
        {
            var Marcacao = _mapper.Map<Marcacao>(marcacao);
            await _repository.AdicionarAsync(Marcacao, cancellationToken);
            return _mapper.Map<MarcacaoDto>(Marcacao);
        }

        public async Task AdicionarMarcacoesEmLoteAsync(IEnumerable<MarcacaoDto> marcacoes, CancellationToken cancellationToken)
        {
            var Macacoes = _mapper.Map<IEnumerable<Marcacao>>(marcacoes);
            await _repository.AdicionarMarcacoesEmLoteAsync(Macacoes, cancellationToken);
        }

        public async Task<List<MarcacaoDto>> ObterPorCpfEPeriodoAsync(ObterPorCpfEPeriodoDto obterPorCpfEPeriodoDto, CancellationToken cancellationToken)
        {
            var marcacoes = await _repository.ObterPorCpfEPeriodoAsync(cpf: obterPorCpfEPeriodoDto.cpf,
            inicio: obterPorCpfEPeriodoDto.inicio, fim: obterPorCpfEPeriodoDto.fim, cancellationToken);
            return _mapper.Map<List<MarcacaoDto>>(marcacoes);
        }

        public async Task<MarcacaoDto?> ObterPorIdAsync(long id, CancellationToken cancellationToken)
        {
            var marcacao = await _repository.ObterPorIdAsync(id, cancellationToken);
            return _mapper.Map<MarcacaoDto?>(marcacao);
        }
    }
}
