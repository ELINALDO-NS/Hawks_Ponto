using HP.Core.Entities;
using HP.Core.Interfaces;
using HP.Data.Context;
using Microsoft.EntityFrameworkCore;


namespace HP.Data.Repository
{
    public class PeriodoRepository(HPContext _context) : IPeriodoRepository
    {
        public async Task<Periodo> AdicionarAsync(Periodo periodo, CancellationToken cancellationToken)
        {
            _context.Periodos.Add(periodo);
            await _context.SaveChangesAsync(cancellationToken);
            return periodo;
        }
        public async Task<Periodo> AtualizarAsync(Periodo periodo, CancellationToken cancellationToken)
        {
            _context.Periodos.Update(periodo);
            await _context.SaveChangesAsync(cancellationToken);
            return periodo;
        }
        public async Task<Periodo?> FecharAsync(Periodo periodo, CancellationToken cancellationToken)
        {
            _context.Periodos.Update(periodo);
            await _context.SaveChangesAsync(cancellationToken);
            return periodo;
        }

        public async Task<Periodo> ObterPeriodoAtualAsync(CancellationToken cancellationToken)
        {
            var peirodo = await _context.Periodos.FirstAsync(x => x.DataFim == new DateOnly(2099, 12, 31));
            return peirodo;
        }

        public async Task<Periodo?> ObterPorIdAsync(int id, CancellationToken cancellationToken)
        {
            var periodo = await _context.Periodos.FindAsync(id, cancellationToken);
            return periodo;
        }
        public async Task<IEnumerable<Periodo>> ObterTodosAsync(CancellationToken cancellationToken)
        {
            var Periodos = await _context.Periodos.ToListAsync(cancellationToken);
            return Periodos;
        }
        public async Task<Periodo?> ReabrirAsync(Periodo periodo, CancellationToken cancellationToken)
        {

            _context.Periodos.Update(periodo);
            await _context.SaveChangesAsync(cancellationToken);
            return periodo;


        }
        public async Task<bool> RemoverAsync(Periodo periodo, CancellationToken cancellationToken)
        {
            _context.Periodos.Remove(periodo);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
