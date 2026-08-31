using Azure.Core;
using HP.Core.Entities;
using HP.Core.Interfaces;
using HP.Data.Context;
using Microsoft.EntityFrameworkCore;


namespace HP.Data.Repository
{
    public class MarcacaoRepository(HPContext _context) : IMarcacaoRepository
    {
        public async Task<Marcacao> AdicionarAsync(Marcacao marcacao, CancellationToken cancellationToken)
        {
            _context.Marcacoes.Add(marcacao);
            await _context.SaveChangesAsync(cancellationToken);
            return marcacao;

        }

        public async Task AdicionarMarcacoesEmLoteAsync(IEnumerable<Marcacao> marcacoes, CancellationToken cancellationToken)
        {
            _context.Marcacoes.AddRange(marcacoes);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<Marcacao>> ObterPorCpfEPeriodoAsync(string cpf, DateTimeOffset inicio, DateTimeOffset fim, CancellationToken cancellationToken)
        {
            var inicioAjustado = new DateTimeOffset(inicio.Year, inicio.Month, inicio.Day, 0, 0, 0, inicio.Offset);
                        
            var fimAjustado = new DateTimeOffset(fim.Year, fim.Month, fim.Day, 23, 59, 59, 999, fim.Offset);

            return await _context.Marcacoes.AsNoTracking()
                .Where(x => x.CPF == cpf && x.DataHora >= inicioAjustado && x.DataHora <= fimAjustado)
                .OrderBy(x => x.DataHora)
                .ToListAsync(cancellationToken);

        }

        public async Task<Marcacao?> ObterPorIdAsync(long id, CancellationToken cancellationToken)
        {

            return await _context.Marcacoes.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id,cancellationToken);
        }
    }
}
