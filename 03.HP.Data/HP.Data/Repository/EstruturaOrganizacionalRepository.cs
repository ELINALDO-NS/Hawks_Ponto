using HP.Core.Entities;
using HP.Core.Interfaces;
using HP.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace HP.Data.Repository
{
    public class EstruturaOrganizacionalRepository(HPContext _context) : IEstruturaOrganizacionalRepository
    {
        public async Task<EstruturaOrganizacional> AdicionarAsync(EstruturaOrganizacional estruturaOrganizacional, CancellationToken cancellationToken)
        {
            _context.EstruturasOrganizacionais.Add(estruturaOrganizacional);
            await _context.SaveChangesAsync(cancellationToken);
            return estruturaOrganizacional;
        }

        public async Task<EstruturaOrganizacional?> AtualizarAsync(EstruturaOrganizacional estruturaOrganizacional, CancellationToken cancellationToken)
        {
            var estruturaOrganizacionalAtual = await _context.EstruturasOrganizacionais.AsNoTracking().FirstOrDefaultAsync(x => x.Id == estruturaOrganizacional.Id);

            if (estruturaOrganizacionalAtual is null)
            {
                return null;
            }

            estruturaOrganizacional.DataCadastro = estruturaOrganizacionalAtual.DataCadastro;
            estruturaOrganizacional.DataUltAtualizacao = DateTimeOffset.Now.ToLocalTime();
            _context.EstruturasOrganizacionais.Update(estruturaOrganizacional);
            await _context.SaveChangesAsync(cancellationToken);
            return estruturaOrganizacional;
        }

        public async Task<EstruturaOrganizacional?> ObterPorIdAsync(int id, CancellationToken cancellationToken)
        {
            var estruturaOrganizacionalAtual = await _context.EstruturasOrganizacionais.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (estruturaOrganizacionalAtual is null)
            {
                return null;
            }
            return estruturaOrganizacionalAtual;
        }

        public async Task<IEnumerable<EstruturaOrganizacional>> ObterTodosAsync(CancellationToken cancellationToken)
        {
            var estruturas = await _context.EstruturasOrganizacionais.ToListAsync();
            if (estruturas is null)
            {
                return [];
            }
            return estruturas;
        }

        public async Task<bool> RemoverAsync(int id, CancellationToken cancellationToken)
        {
            var estruturaOrganizacional = await _context.EstruturasOrganizacionais.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (estruturaOrganizacional is null)
            {
                return false;
            }
            _context.EstruturasOrganizacionais.Remove(estruturaOrganizacional);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }

}
