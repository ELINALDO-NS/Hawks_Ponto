using HP.Core.Entities;
using HP.Data.Context;
using Microsoft.EntityFrameworkCore;
using HP.Core.Interfaces;


namespace HP.Data.Repository
{
    public class PessoaRepository(HPContext _context) : IPessoaRepository
    {
        public async Task<Pessoa> AdicionarAsync(Pessoa pessoa, CancellationToken cancellationToken)
        {

            _context.Pessoas.Add(pessoa);
            await _context.SaveChangesAsync(cancellationToken);
            await _context.Entry(pessoa).Reference(p => p.Estrutura).LoadAsync(cancellationToken);
            await _context.Entry(pessoa).Reference(p => p.Cargo).LoadAsync(cancellationToken);
            await _context.Entry(pessoa).Reference(p => p.Endereco).LoadAsync(cancellationToken);

            return pessoa;
        }

        public async Task<Pessoa?> AtualizarAsync(Pessoa pessoa, CancellationToken cancellationToken)
        {
            // 1. Busca a pessoa e já inclui os relacionamentos em uma ÚNICA consulta
            var pessoaAtual = await _context.Pessoas
                .Include(p => p.Estrutura)
                .Include(p => p.Cargo)
                .Include(p => p.Endereco)
                .FirstOrDefaultAsync(x => x.Id == pessoa.Id, cancellationToken);

            if (pessoaAtual is null)
            {
                return null;
            }

            // 2. Preserva campos que não devem ser sobrescritos
            pessoa.EnderecoId = pessoaAtual.EnderecoId;
            pessoa.DataCadastro = pessoaAtual.DataCadastro;
            pessoa.DataUltAtualizacao = DateTime.UtcNow;

            // 3. Atualiza as propriedades escalares da Pessoa
            _context.Entry(pessoaAtual).CurrentValues.SetValues(pessoa);

            // 4. Trata a atualização ou inserção do Endereço com segurança
            if (pessoa.Endereco is not null)
            {
                if (pessoaAtual.Endereco is not null)
                {
                    pessoa.Endereco.Id = pessoaAtual.Endereco.Id;
                    _context.Entry(pessoaAtual.Endereco).CurrentValues.SetValues(pessoa.Endereco);
                }
                else
                {
                    // Adiciona novo endereço caso a pessoa não tivesse um antes
                    pessoa.Endereco.Id = 0;
                    pessoaAtual.Endereco = pessoa.Endereco;
                }
            }

            // 5. Salva todas as alterações no banco de dados de uma vez
            await _context.SaveChangesAsync(cancellationToken);

            return pessoaAtual;
        }

        public async Task<Pessoa?> ObterPorIdAsync(int id, CancellationToken cancellationToken)
        {
            var Pessoa = await _context.Pessoas
                .Include(x => x.Endereco)
                .Include(x => x.Estrutura)
                .Include(x => x.Cargo)
                .AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (Pessoa == null) { return null; }

            return Pessoa;
        }

        public async Task<IEnumerable<Pessoa>> ObterTodosAsync(CancellationToken cancellationToken)
        {
            var Pessoas = await _context.Pessoas
                .Include(x => x.Endereco)
                .Include(x => x.Estrutura)
                .Include(x => x.Cargo)
                .AsNoTracking().ToListAsync(cancellationToken);
            return Pessoas;
        }

        public async Task<bool> RemoverAsync(int id, CancellationToken cancellationToken)
        {
            var Pessoa = await _context.Pessoas.Include(x=>x.Endereco).FirstOrDefaultAsync(x=> x.Id == id);

            if (Pessoa == null) { return false; }
            if(Pessoa.Endereco is not null)
    {
                _context.Enderecos.Remove(Pessoa.Endereco);
            }
           
            _context.Pessoas.Remove(Pessoa);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }


    }
}
