using HP.Core.Entities;
using HP.Core.Interfaces;
using HP.Manager.DTOs.EstruturaOrganizacional;
using HP.Manager.Interfaces;
using MapsterMapper;


namespace HP.Manager.Implementation
{
    public class EstruturaOrganizacionalManager(IEstruturaOrganizacionalRepository _repository, IMapper _mapper) : IEstruturaOrganizacionalManager
    {
        public async Task<EstruturaOrganizacionalDto> AdicionarAsync(AdicionaEstruturaOrganizacionalDto estruturaOrganizacional, CancellationToken cancellationToken)
        {
           
            var estrutura = _mapper.Map<EstruturaOrganizacional>(estruturaOrganizacional);           
            await _repository.AdicionarAsync(estrutura, cancellationToken);
            return _mapper.Map<EstruturaOrganizacionalDto>(estrutura);
        }

        public async Task<EstruturaOrganizacionalDto> AtualizarAsync(AtualizaEstruturaOrganizacionalDto estruturaOrganizacional, CancellationToken cancellationToken)
        {
            var estruturaAtualizada = _mapper.Map<EstruturaOrganizacional>(estruturaOrganizacional);

            await _repository.AtualizarAsync(estruturaAtualizada, cancellationToken);

            return _mapper.Map<EstruturaOrganizacionalDto>(estruturaAtualizada);
        }

        public async Task<EstruturaOrganizacionalDto?> ObterPorIdAsync(int id, CancellationToken cancellationToken)
        {
            var estrutura = await _repository.ObterPorIdAsync(id, cancellationToken);
            if (estrutura is null)
            {
                return null;
            }
            return _mapper.Map<EstruturaOrganizacionalDto>(estrutura);
        }

        public async Task<IEnumerable<EstruturaOrganizacionalDto>> ObterTodosAsync(CancellationToken cancellationToken)
        {
            var Estruturas = await _repository.ObterTodosAsync(cancellationToken);

            var EstruturasDto = _mapper.Map<List<EstruturaOrganizacionalDto>>(Estruturas);

            return EstruturasDto;
        }

        public async Task<bool> RemoverAsync(int id, CancellationToken cancellationToken)
        {
            var excluido = await _repository.RemoverAsync(id, cancellationToken);
            return excluido;
        }
    }
}
