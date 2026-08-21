using HP.Core.Entities;
using HP.Core.Interfaces;
using HP.Data.Context;
using Microsoft.EntityFrameworkCore;


namespace HP.Data.Repository
{
    public class HorarioRepository(HPContext _context) : IHorarioRepository
    {
        public async Task<Horario> AdicionarAsync(Horario horario, CancellationToken cancellationToken)
        {
            _context.Horarios.Add(horario);
            await _context.SaveChangesAsync(cancellationToken);
            return horario;
        }

        public async Task<Horario?> AtualizarAsync(Horario horario, CancellationToken cancellationToken)
        {
            var horarioAtual = await _context.Horarios
                 .Include(x => x.Jornadas)
                 .FirstOrDefaultAsync(x => x.Id == horario.Id, cancellationToken);

            if (horarioAtual is null)
            {
                return null;
            }
            
            horario.DataCadastro = horarioAtual.DataCadastro;
            horario.DataUltAtualizacao = DateTimeOffset.Now.ToLocalTime();
            _context.Entry(horarioAtual).CurrentValues.SetValues(horario);

            AtualizarJornadas(horarioAtual, horario.Jornadas);

            await _context.SaveChangesAsync(cancellationToken);
            return horarioAtual;
        }
        private void AtualizarJornadas(Horario horarioAtual, ICollection<Jornada> jornadasRecebidas)
        {
            var idsRecebidos = jornadasRecebidas
                .Where(j => j.Id > 0)
                .Select(j => j.Id)
                .ToList();
            
            var jornadasParaRemover = horarioAtual.Jornadas
                .Where(j => !idsRecebidos.Contains(j.Id))
                .ToList();

            if (jornadasParaRemover.Count > 0)
            {
                _context.Jornadas.RemoveRange(jornadasParaRemover);
            }
            
            foreach (var jornadaRecebida in jornadasRecebidas)
            {
                jornadaRecebida.HorarioId = horarioAtual.Id;

                if (jornadaRecebida.Id == 0)
                {
                    horarioAtual.Jornadas.Add(jornadaRecebida);
                    continue;
                }

                var jornadaExistente = horarioAtual.Jornadas
                    .FirstOrDefault(j => j.Id == jornadaRecebida.Id);

                if (jornadaExistente != null)
                {
                    _context.Entry(jornadaExistente).CurrentValues.SetValues(jornadaRecebida);
                }
            }
        }
        public async Task<Horario?> ObterPorIdAsync(int id, CancellationToken cancellationToken)
        {
            var horario = await _context.Horarios.Include(x => x.Jornadas).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (horario == null) { return null; }

            return horario;
        }

        public async Task<IEnumerable<Horario>> ObterTodosAsync(CancellationToken cancellationToken)
        {
            var horarios = await _context.Horarios.Include(x => x.Jornadas).AsNoTracking().ToListAsync(cancellationToken);
            return horarios;
        }

        public async Task<bool> RemoverAsync(int id, CancellationToken cancellationToken)
        {
            var horario = _context.Horarios.Find(id);
            if (horario is null) { return false; }
            _context.Horarios.Remove(horario);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
