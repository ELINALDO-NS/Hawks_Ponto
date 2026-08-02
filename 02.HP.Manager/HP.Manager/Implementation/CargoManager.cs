using HP.Core.Entities;
using HP.Core.Interfaces;
using HP.Manager.DTOs.Cargo;
using HP.Manager.Interfaces.Managers;
using MapsterMapper;


namespace HP.Manager.Implementation
{
    public class CargoManager(ICargoRepository _repository,IMapper _mapper) : ICargoManager
    {

        public async Task<CargoDto> AdicionarAsync(AdicionaCargoDto cargo, CancellationToken cancellationToken)
        {
            var Cargo = _mapper.Map<Cargo>(cargo);
            await _repository.AdicionarAsync(Cargo, cancellationToken);
            return _mapper.Map<CargoDto>(Cargo);
        }
          

        public async Task<CargoDto> AtualizarAsync(AtualizaCargoDto cargo, CancellationToken cancellationToken)
        {
            var cargoAtualizada = _mapper.Map<Cargo>(cargo);

            await _repository.AtualizarAsync(cargoAtualizada, cancellationToken);

            return _mapper.Map<CargoDto>(cargoAtualizada);
        
        }

        public async Task<CargoDto?> ObterPorIdAsync(int id, CancellationToken cancellationToken)
        {
            var cargo = await _repository.ObterPorIdAsync(id, cancellationToken);
            if (cargo is null)
            {
                return null;
            }
            return _mapper.Map<CargoDto>(cargo);
        }

        public async Task<IEnumerable<CargoDto>> ObterTodosAsync(CancellationToken cancellationToken)
        {
            var cargos = await _repository.ObterTodosAsync(cancellationToken);

            var cargosDto = _mapper.Map<List<CargoDto>>(cargos);

            return cargosDto;
        }

        public async Task<bool> RemoverAsync(int id, CancellationToken cancellationToken)
        {
            var excluido = await _repository.RemoverAsync(id, cancellationToken);
            return excluido;
        }
    }
}
