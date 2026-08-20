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
            await _context.Entry(pessoa)
                .Collection(p => p.Cargos)
                .Query()
                .Include(cp => cp.Cargo)
                .LoadAsync(cancellationToken);

            return pessoa;
        }

        public async Task<Pessoa?> AtualizarAsync(Pessoa pessoa, CancellationToken cancellationToken)
        {
            var pessoaAtual = await _context.Pessoas
                .Include(p => p.Estrutura)
                .Include(p => p.Cargos)
                .ThenInclude(cp => cp.Cargo)
                .Include(p => p.Endereco)
                .FirstOrDefaultAsync(x => x.Id == pessoa.Id, cancellationToken);

            if (pessoaAtual is null)
            {
                return null;
            }

            pessoa.EnderecoId = pessoaAtual.EnderecoId;
            pessoa.DataCadastro = pessoaAtual.DataCadastro;
            pessoa.DataUltAtualizacao = DateTime.UtcNow;

            _context.Entry(pessoaAtual).CurrentValues.SetValues(pessoa);
            AtualizarEndereco(pessoaAtual, pessoa.Endereco);
            AtualizarCargo(pessoaAtual, pessoa.Cargos);
            await _context.SaveChangesAsync(cancellationToken);

            return pessoaAtual;
        }
        private void AtualizarEndereco(Pessoa pessoaAtual, Endereco? enderecoNovo)
        {
            if (pessoaAtual.Endereco is not null && enderecoNovo is not null)
            {
                enderecoNovo.Id = pessoaAtual.Endereco.Id;
                _context.Entry(pessoaAtual.Endereco).CurrentValues.SetValues(enderecoNovo);
            }
            else if (enderecoNovo is not null)
            {
                enderecoNovo.Id = 0;
                pessoaAtual.Endereco = enderecoNovo;
            }
        }
        private void AtualizarCargo(Pessoa pessoaAtual, ICollection<CargoPessoa> cargoNovo)
        {
            if (!cargoNovo.Any())
            {
                return;
            }
            var cargo = cargoNovo.Single();
            var cargoVigente = pessoaAtual.Cargos.FirstOrDefault(c => c.DataFim is null);
            if (cargoVigente is not null)
            {
                cargoVigente.DataFim = cargo.DataInicio;
            }
            pessoaAtual.Cargos.Add(new CargoPessoa
            {
                CargoId = cargo.CargoId,
                DataInicio = cargo.DataInicio,
                DataFim = null
            });
        }

        public async Task<Pessoa?> ObterPorIdAsync(int id, CancellationToken cancellationToken)
        {
            var Pessoa = await _context.Pessoas
                .Include(x => x.Endereco)
                .Include(x => x.Estrutura)
                .Include(x => x.Cargos)
                .ThenInclude(cp => cp.Cargo)
                .AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (Pessoa == null) { return null; }

            return Pessoa;
        }

        public async Task<IEnumerable<Pessoa>> ObterTodosAsync(CancellationToken cancellationToken)
        {
            var Pessoas = await _context.Pessoas
                .Include(x => x.Endereco)
                .Include(x => x.Estrutura)
                .Include(x => x.Cargos)
                .ThenInclude(cp => cp.Cargo)
                .AsNoTracking().ToListAsync(cancellationToken);
            return Pessoas;
        }

        public async Task<bool> RemoverAsync(int id, CancellationToken cancellationToken)
        {
            var Pessoa = await _context.Pessoas.Include(x => x.Endereco).FirstOrDefaultAsync(x => x.Id == id);

            if (Pessoa == null) { return false; }
            if (Pessoa.Endereco is not null)
            {
                _context.Enderecos.Remove(Pessoa.Endereco);
            }

            _context.Pessoas.Remove(Pessoa);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }


    }
}
