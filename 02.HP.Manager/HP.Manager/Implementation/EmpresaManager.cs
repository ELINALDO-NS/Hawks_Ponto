using HP.Core;
using HP.Manager.DTOs.Empresa;
using HP.Manager.Interfaces.Managers;
using HP.Manager.Interfaces.Repository;
using MapsterMapper;


namespace HP.Manager.Implementation
{
    public class EmpresaManager(IEmpresaRepository _repository, IMapper _mapper) : IEmpresaManager
    {
        public async Task<EmpresaDto> AdicionarAsync(AdicionaEmpresaDto empresa, CancellationToken cancellationToken)
        {

            var novaempresa = _mapper.Map<Empresa>(empresa);

            await _repository.AdicionarAsync(novaempresa, cancellationToken);

            return _mapper.Map<EmpresaDto>(novaempresa);

        }

        public async Task<EmpresaDto> AtualizarAsync(AtualizaEmpresaDto empresa, CancellationToken cancellationToken)
        {

            var empresaatualizada = _mapper.Map<Empresa>(empresa);

            await _repository.AtualizarAsync(empresaatualizada, cancellationToken);

            return _mapper.Map<EmpresaDto>(empresaatualizada);

        }

        public async Task<EmpresaDto?> ObterPorIdAsync(int id, CancellationToken cancellationToken)
        {
            var empresa = await _repository.ObterPorIdAsync(id, cancellationToken);
            if (empresa is null)
            {
                return null;
            }
            return _mapper.Map<EmpresaDto>(empresa);
        }

        public async Task<IEnumerable<EmpresaDto>> ObterTodosAsync(CancellationToken cancellationToken)
        {
            var empresas = await _repository.ObterTodosAsync(cancellationToken);

            var empresasDto = _mapper.Map<List<EmpresaDto>>(empresas);

            return empresasDto;
        }

        public async Task<bool> RemoverAsync(int id, CancellationToken cancellationToken)
        {
          var excluido =  await _repository.RemoverAsync(id, cancellationToken);
            return excluido;
        }
    }
}
