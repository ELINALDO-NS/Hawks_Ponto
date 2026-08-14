using HP.Core.Entities;
using HP.Core.Interfaces;
using HP.Manager.DTOs.Horario;
using HP.Manager.Interfaces;
using MapsterMapper;


namespace HP.Manager.Implementation
{
    public class HorarioManager(IHorarioRepository _repository,IMapper _mapper) : IHorarioManager
    {
        public async Task<HorarioDto> AdicionarAsync(AdicionaHorarioDto horariodto, CancellationToken cancellationToken)
        {
            var horario = _mapper.Map<Horario>(horariodto);
            await _repository.AdicionarAsync(horario, cancellationToken);
            return _mapper.Map<HorarioDto>(horario);
        }

        public async Task<HorarioDto> AtualizarAsync(AtualizaHorarioDto horariodto, CancellationToken cancellationToken)
        {
            var horariodtoAtualizado = _mapper.Map<Horario>(horariodto);

            await _repository.AtualizarAsync(horariodtoAtualizado, cancellationToken);

            return _mapper.Map<HorarioDto>(horariodtoAtualizado);
        }

        public async Task<HorarioDto?> ObterPorIdAsync(int id, CancellationToken cancellationToken)
        {
            var horario = await _repository.ObterPorIdAsync(id, cancellationToken);
            if (horario is null)
            {
                return null;
            }
            return _mapper.Map<HorarioDto>(horario);
        }

        public async Task<IEnumerable<HorarioDto>> ObterTodosAsync(CancellationToken cancellationToken)
        {
            var horarios = await _repository.ObterTodosAsync(cancellationToken);

            var horariosDto = _mapper.Map<List<HorarioDto>>(horarios);

            return horariosDto;
        }

        public async Task<bool> RemoverAsync(int id, CancellationToken cancellationToken)
        {
            var excluido = await _repository.RemoverAsync(id, cancellationToken);
            return excluido;
        }
    }
}
