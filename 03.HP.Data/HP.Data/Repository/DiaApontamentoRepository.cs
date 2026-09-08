using HP.Core.Entities;
using HP.Core.Interfaces;
using HP.Data.Context;
using Microsoft.EntityFrameworkCore;


namespace HP.Data.Repository
{
    public class DiaApontamentoRepository(HPContext _context) : IDiaApontamentoRepository
    {
        public async Task AdicionarAsync(DiaApontamento diaApontamento, CancellationToken cancellationToken)
        {
            _context.DiaApontamentos.Add(diaApontamento);
            await _context.SaveChangesAsync();
        }

        public async Task AdicionarDiasAsync(IEnumerable<DiaApontamento> diaApontamento, CancellationToken cancellationToken)
        {
            await _context.AddRangeAsync(diaApontamento, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task AtualizarAsync(DiaApontamento diaApontamento, CancellationToken cancellationToken)
        {
            var diaApontamentoAtual = await _context.DiaApontamentos.AsNoTracking().FirstOrDefaultAsync(x => x.Id == diaApontamento.Id);
            if (diaApontamentoAtual is null)
            {
                return;
            }
            diaApontamento.DataUltAtualizacao = diaApontamentoAtual.DataUltAtualizacao;
            _context.Entry(diaApontamentoAtual).CurrentValues.SetValues(diaApontamento);
            _context.Update(diaApontamentoAtual);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<DiaApontamento?> ObterPorIdAsync(long id, CancellationToken cancellationToken)
        {
            var diaApontamentoAtual = await _context.DiaApontamentos.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (diaApontamentoAtual is null)
            {
                return null;
            }
            return diaApontamentoAtual;
        }

        public async Task<IReadOnlyCollection<DiaApontamento>> ObterPorPessoaEPeriodoAsync(int pessoaId, DateOnly dataInicio, DateOnly dataFim, CancellationToken cancellationToken)
        {
            return await _context.DiaApontamentos.AsNoTracking()
                .Where(x => x.PessoaId == pessoaId && x.DataApontamento >= dataInicio && x.DataApontamento <= dataFim)
                .OrderBy(x => x.DataApontamento)
                .ToListAsync(cancellationToken);
        }

        public async Task<HashSet<DateOnly>?> ObterPorPessoaIdAsync(int PessoaId, CancellationToken cancellationToken)
        {
            var diasApontamentos = await _context.DiaApontamentos
                .AsNoTracking()
                .Where(x => x.PessoaId == PessoaId)
                .Select(x => x.DataApontamento)
                .ToListAsync(cancellationToken);

            var datasExistentesSet = diasApontamentos.ToHashSet();
            if (diasApontamentos is null)
            {
                return null;
            }
            return datasExistentesSet;
        }

        public async Task<bool> RemoverAsync(int PessoaId, IEnumerable<DateOnly> DiasDelete, CancellationToken cancellationToken)
        {
            var diaApontamentoAtual = await _context.DiaApontamentos
                .AsNoTracking()
                .Where(x => x.PessoaId == PessoaId && DiasDelete.Contains(x.DataApontamento)).ToListAsync();

            if (diaApontamentoAtual is null)
            {
                return false;
            }
            _context.DiaApontamentos.RemoveRange(diaApontamentoAtual);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
