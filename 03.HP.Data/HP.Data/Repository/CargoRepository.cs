using HP.Core.Entities;
using HP.Core.Interfaces;
using HP.Data.Context;
using Microsoft.EntityFrameworkCore;


namespace HP.Data.Repository
{
    public class CargoRepository(HPContext _context) : ICargoRepository
    {
        public async Task<Cargo> AdicionarAsync(Cargo cargo, CancellationToken cancellationToken)
        {
            _context.Cargos.Add(cargo);
            await _context.SaveChangesAsync(cancellationToken);
            return cargo;
        }

        public async Task<Cargo?> AtualizarAsync(Cargo cargo, CancellationToken cancellationToken)
        {
            var cargoAtual = await _context.Cargos.AsNoTracking().FirstOrDefaultAsync(x => x.Id == cargo.Id);
            if (cargoAtual is null)
            {
                return null;
            }

            cargo.DataCadastro = cargoAtual.DataCadastro;
            cargo.DataUltAtualizacao = DateTimeOffset.Now.ToLocalTime();
            _context.Entry(cargoAtual).CurrentValues.SetValues(cargo);
            _context.Update(cargoAtual);
            await _context.SaveChangesAsync(cancellationToken);
            return cargoAtual;
        }

        public async Task<Cargo?> ObterPorIdAsync(int id, CancellationToken cancellationToken)
        {
            var cargo = await _context.Cargos.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (cargo == null) { return null; }

            return cargo;
        }

        public async Task<IEnumerable<Cargo>> ObterTodosAsync(CancellationToken cancellationToken)
        {
            var cargos = await _context.Cargos.AsNoTracking().ToListAsync(cancellationToken);
            return cargos;
        }

        public async Task<bool> RemoverAsync(int id, CancellationToken cancellationToken)
        {
            var cargo = _context.Cargos.Find(id);
            if (cargo is null) { return false; }
            _context.Cargos.Remove(cargo);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
