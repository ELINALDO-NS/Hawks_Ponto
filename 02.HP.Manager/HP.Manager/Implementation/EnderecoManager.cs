using HP.Core;
using HP.Manager.DTOs.Endereco;
using HP.Manager.Interfaces.Managers;
using HP.Manager.Interfaces.Repositories;
using MapsterMapper;

namespace HP.Manager.Implementation
{
    public class EnderecoManager(IEnderecoRepository _repository, IMapper _mapper) : IEnderecoManager
    {
        public async Task<EnderecoDto> AdicionarAsync(AdicionaEnderecoDto endereco, CancellationToken cancellationToken)
        {
            var Novoendereco = _mapper.Map<Endereco>(endereco);
            await _repository.AdicionarAsync(Novoendereco, cancellationToken);
            return _mapper.Map<EnderecoDto>(Novoendereco);
        }

        public async Task<EnderecoDto> AtualizarAsync(AtualizaEnderecoDto endereco, CancellationToken cancellationToken)
        {
            var atualizaendereco = _mapper.Map<Endereco>(endereco);
            await _repository.AtualizarAsync(atualizaendereco, cancellationToken);
            return _mapper.Map<EnderecoDto>(atualizaendereco);
        }

        public async Task<EnderecoDto?> ObterPorIdAsync(int id, CancellationToken cancellationToken)
        {
           var endereco = await _repository.ObterPorIdAsync(id,cancellationToken);
            if (endereco is null) return null;
            return _mapper.Map<EnderecoDto>(endereco);
        }

        public async Task<IEnumerable<EnderecoDto>> ObterTodosAsync(CancellationToken cancellationToken)
        {
            var enderecos = await _repository.ObterTodosAsync(cancellationToken);
            if (enderecos is null)
            {
                return [];
            }
            return _mapper.Map<List<EnderecoDto>>(enderecos);
        }

        public async Task<bool> RemoverAsync(int id, CancellationToken cancellationToken)
        {
            var excluido = await _repository.RemoverAsync(id,cancellationToken);
            if (!excluido)
            {
                return false;
            }
            return true;
        }
    }
}
