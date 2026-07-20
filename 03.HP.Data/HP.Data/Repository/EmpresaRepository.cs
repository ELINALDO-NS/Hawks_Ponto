using Azure.Core;
using HP.Core;
using HP.Data.Context;
using HP.Manager.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HP.Data.Repository
{
    public class EmpresaRepository(HPContext _context) : IEmpresaRepository
    {

        public async Task<Empresa> AdicionarAsync(Empresa empresa, CancellationToken cancellationToken)
        {
            _context.Empresa.Add(empresa);
            await _context.SaveChangesAsync(cancellationToken);
            return empresa;
        }

        public async Task<Empresa?> AtualizarAsync(Empresa empresa, CancellationToken cancellationToken)
        {
            var empresaAtual = await _context.Empresa.AsNoTracking().FirstOrDefaultAsync(x=> x.Id == empresa.Id);

            if (empresaAtual is null)
            {
                return null;
            }
            
            empresa.DataCadastro = empresaAtual.DataCadastro;
            _context.Empresa.Update(empresa);
            await _context.SaveChangesAsync(cancellationToken);
            return empresa;
        }

        public async Task<Empresa?> ObterPorIdAsync(int id, CancellationToken cancellationToken)
        {
            var empresa = await _context.Empresa.FindAsync(id);

            if (empresa == null) { return null; }

            return empresa;
        }

        public async Task<IEnumerable<Empresa>> ObterTodosAsync(CancellationToken cancellationToken)
        {
            var empresas = await _context.Empresa.ToListAsync(cancellationToken);
            return empresas;
        }

        public async Task<bool> RemoverAsync(int id, CancellationToken cancellationToken)
        {
            var empresa = _context.Empresa.Find(id);

            if (empresa == null) { return false; }
            _context.Empresa.Remove(empresa);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }


    }
}
