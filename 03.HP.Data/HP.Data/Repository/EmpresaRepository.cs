
using HP.Core.Entities;
using HP.Data.Context;
using HP.Core.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;


namespace HP.Data.Repository
{
    public class EmpresaRepository(HPContext _context) : IEmpresaRepository
    {

        public async Task<Empresa> AdicionarAsync(Empresa empresa, CancellationToken cancellationToken)
        {
            _context.Empresas.Add(empresa);
            await _context.SaveChangesAsync(cancellationToken);
            return empresa;
        }

        public async Task<Empresa?> AtualizarAsync(Empresa empresa, CancellationToken cancellationToken)
        {
            var empresaAtual = await _context.Empresas.AsNoTracking().FirstOrDefaultAsync(x => x.Id == empresa.Id);

            if (empresaAtual is null)
            {
                return null;
            }

            empresa.DataCadastro = empresaAtual.DataCadastro;
            empresa.DataUltAtualizacao = DateTime.UtcNow;
            _context.Empresas.Update(empresa);
            await _context.SaveChangesAsync(cancellationToken);
            return empresa;
        }

        public async Task<Empresa?> ObterPorIdAsync(int id, CancellationToken cancellationToken)
        {
            var empresa = await _context.Empresas.Include(x => x.Endrereco).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (empresa == null) { return null; }

            return empresa;
        }

        public async Task<IEnumerable<Empresa>> ObterTodosAsync(CancellationToken cancellationToken)
        {
            var empresas = await _context.Empresas.Include(x => x.Endrereco).ToListAsync(cancellationToken);
            return empresas;
        }

        public async Task<bool> RemoverAsync(int id, CancellationToken cancellationToken)
        {
            var empresa = _context.Empresas.Find(id);

            if (empresa == null) { return false; }
            _context.Empresas.Remove(empresa);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }


    }
}
