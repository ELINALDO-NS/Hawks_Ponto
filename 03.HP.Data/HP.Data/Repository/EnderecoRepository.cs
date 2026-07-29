using HP.Core;
using HP.Data.Context;
using HP.Manager.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HP.Data.Repository
{
    public class EnderecoRepository(HPContext _context) : IEnderecoRepository
    {
        public async Task<Endereco> AdicionarAsync(Endereco endereco, CancellationToken cancellationToken)
        {
            _context.Endereco.Add(endereco);
            await _context.SaveChangesAsync(cancellationToken);
            return endereco;
        }

        public async Task<Endereco?> AtualizarAsync(Endereco endereco, CancellationToken cancellationToken)
        {
            var enderecoAtual = await _context.Endereco.AsNoTracking().FirstOrDefaultAsync(x => x.Id == endereco.Id);

            if (enderecoAtual is null)
            {
                return null;
            }

            _context.Endereco.Update(endereco);
            await _context.SaveChangesAsync(cancellationToken);
            return endereco;
        }

        public async Task<Endereco?> ObterPorIdAsync(int id, CancellationToken cancellationToken)
        {
            var endereco = await _context.Endereco.FindAsync(id);

            if (endereco == null) { return null; }

            return endereco;
        }

        public async Task<IEnumerable<Endereco>> ObterTodosAsync(CancellationToken cancellationToken)
        {
            var endereco = await _context.Endereco.ToListAsync(cancellationToken);
            return endereco;
        }

        public async Task<bool> RemoverAsync(int id, CancellationToken cancellationToken)
        {
            var endereco = _context.Endereco.Find(id);

            if (endereco == null) { return false; }
            _context.Endereco.Remove(endereco);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
