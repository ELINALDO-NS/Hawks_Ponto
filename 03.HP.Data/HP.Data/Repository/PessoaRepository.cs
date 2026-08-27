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
                .AsSplitQuery()
                .LoadAsync(cancellationToken);

            await _context.Entry(pessoa)
                .Collection(p => p.EstruturasOrganizacionais)
                .Query()
                .Include(cp => cp.EstruturaOrganizacional)
                .AsSplitQuery()
                .LoadAsync(cancellationToken);

            await _context.Entry(pessoa)
               .Collection(p => p.Horarios)
               .Query()
               .Include(cp => cp.Horario)
               .AsSplitQuery()
               .LoadAsync(cancellationToken);

            return pessoa;
        }
        public async Task<Pessoa?> AtualizarAsync(Pessoa pessoa, CancellationToken cancellationToken)
        {
            var pessoaAtual = await _context.Pessoas
                .Include(p => p.Horarios)
                .ThenInclude(cp => cp.Horario)
                .Include(p => p.EstruturasOrganizacionais)
                .ThenInclude(cp => cp.EstruturaOrganizacional)
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
            pessoa.DataUltAtualizacao = DateTimeOffset.Now.ToLocalTime();

            _context.Entry(pessoaAtual).CurrentValues.SetValues(pessoa);
            AtualizarEndereco(pessoaAtual, pessoa.Endereco);
            await AtualizarCargo(pessoaAtual, pessoa.Cargos, cancellationToken);
            await AtualizarEstrturaOrganizacional(pessoaAtual, pessoa.EstruturasOrganizacionais, cancellationToken);
            await AtualizarHorario(pessoaAtual, pessoa.Horarios, cancellationToken);
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
        private async Task AtualizarCargo(Pessoa pessoaAtual, ICollection<CargoPessoa> cargoNovo, CancellationToken cancellationToken)
        {
            if (!cargoNovo.Any())
            {
                return;
            }
            var cargo = cargoNovo.Single();
            var cargoVigente = pessoaAtual.Cargos.FirstOrDefault(c => c.DataFim is null);
            var cargoDb = await _context.Cargos.FindAsync(cargo.CargoId, cancellationToken);

            if (cargoVigente is not null)
            {
                cargoVigente.DataFim = cargo.DataInicio;
            }
            pessoaAtual.Cargos.Add(new CargoPessoa
            {
                CargoId = cargo.CargoId,
                DataInicio = cargo.DataInicio,
                Cargo = cargoDb,
                DataFim = null
            });
        }
        private async Task AtualizarHorario(Pessoa pessoaAtual, ICollection<HorarioPessoa> horarioNova, CancellationToken cancellationToken)
        {
            if (!horarioNova.Any())
            {
                return;
            }
            var horario = horarioNova.Single();
            var HorarioVigente = pessoaAtual.Horarios.FirstOrDefault(c => c.DataFim is null);
            if (HorarioVigente is not null)
            {
                HorarioVigente.DataFim = horario.DataInicio;
            }
            var horarioDb = await _context.Horarios.FindAsync(horario.HorarioId, cancellationToken);

            pessoaAtual.Horarios.Add(new HorarioPessoa
            {
                HorarioId = horario.HorarioId,
                DataInicio = horario.DataInicio,
                Horario = horarioDb!,
                DataFim = null
            });
        }
        private async Task AtualizarEstrturaOrganizacional(Pessoa pessoaAtual, ICollection<EstruturaOrganizacionalPessoa> EstruturaOrganizacionalNova, CancellationToken cancellationToken)
        {
            if (EstruturaOrganizacionalNova is null || !EstruturaOrganizacionalNova.Any())
            {
                return;
            }
            var EstruturaOrganizacional = EstruturaOrganizacionalNova.Single();
            var EstruturaOrganizacionalVigente = pessoaAtual.EstruturasOrganizacionais.FirstOrDefault(c => c.DataFim is null);
            if (EstruturaOrganizacionalVigente is not null)
            {
                EstruturaOrganizacionalVigente.DataFim = EstruturaOrganizacional.DataInicio;
            }
            var estruturaDb = await _context.EstruturasOrganizacionais.FindAsync(EstruturaOrganizacional.EstruturaOrganizacionalId, cancellationToken);

            pessoaAtual.EstruturasOrganizacionais.Add(new EstruturaOrganizacionalPessoa
            {
                EstruturaOrganizacionalId = EstruturaOrganizacional.EstruturaOrganizacionalId,
                DataInicio = EstruturaOrganizacional.DataInicio,
                EstruturaOrganizacional = estruturaDb!,
                DataFim = null
            });
        }
        public async Task<Pessoa?> ObterPorIdAsync(int id, CancellationToken cancellationToken)
        {
            var Pessoa = await _context.Pessoas
                .Include(x => x.Endereco)
                .Include(x => x.Horarios)
                .ThenInclude(cp => cp.Horario)
                .Include(x => x.EstruturasOrganizacionais)
                .ThenInclude(cp => cp.EstruturaOrganizacional)
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
                .Include(x => x.Horarios)
                .ThenInclude(cp => cp.Horario)
                .Include(x => x.EstruturasOrganizacionais)
                .ThenInclude(cp => cp.EstruturaOrganizacional)
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
                _context.Entry(Pessoa.Endereco).State = EntityState.Deleted;
            }

            _context.Pessoas.Remove(Pessoa);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<Pessoa?> ObterPorMatriculaAsync(int Matricula, CancellationToken cancellationToken)
        {
            var Pessoa = await _context.Pessoas
                 .AsNoTracking().FirstOrDefaultAsync(x => x.Matricula == Matricula);

            if (Pessoa == null) { return null; }

            return Pessoa;
        }
    }
}
