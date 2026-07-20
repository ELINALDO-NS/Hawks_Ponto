using HP.Core;
using HP.Manager.DTOs.Empresa;
using HP.Manager.Interfaces.Managers;
using HP.Manager.Interfaces.Repository;


namespace HP.Manager.Implementation
{
    public class EmpresaManager(IEmpresaRepository _repository) : IEmpresaManager
    {
        public async Task<EmpresaDto> AdicionarAsync(AdicionaEmpresaDto empresa, CancellationToken cancellationToken)
        {

            var novaempresa = new Empresa(empresa.IdEndrereco, empresa.Codigo, empresa.RazaoSocial,
                empresa.CnpjCpf, empresa.Telefone, empresa.Site, empresa.Email,
                empresa.TipoEmpresa, empresa.Portaria1510, empresa.Portaria671);

            await _repository.AdicionarAsync(novaempresa, cancellationToken);

            return new EmpresaDto(novaempresa.Id, novaempresa.IdEndrereco, novaempresa.Codigo, novaempresa.RazaoSocial, novaempresa.CnpjCpf, novaempresa.Telefone, novaempresa.Site, novaempresa.Email, novaempresa.TipoEmpresa, novaempresa.DataCadastro, novaempresa.DataUltAtualizacao, novaempresa.Portaria1510, novaempresa.Portaria671);

        }

        public async Task<EmpresaDto> AtualizarAsync(AtualizaEmpresaDto empresa, CancellationToken cancellationToken)
        {

            var empresaatualizada = new Empresa(empresa.Id, empresa.IdEndrereco, empresa.Codigo, empresa.RazaoSocial,
             empresa.CnpjCpf, empresa.Telefone, empresa.Site, empresa.Email,
             empresa.TipoEmpresa, empresa.Portaria1510, empresa.Portaria671);

            await _repository.AtualizarAsync(empresaatualizada, cancellationToken);

            return new EmpresaDto(empresaatualizada.Id, empresaatualizada.IdEndrereco, empresaatualizada.Codigo,
                empresaatualizada.RazaoSocial,
                empresaatualizada.CnpjCpf, empresaatualizada.Telefone,
                empresaatualizada.Site, empresaatualizada.Email, empresaatualizada.TipoEmpresa,
                empresaatualizada.DataCadastro, empresaatualizada.DataUltAtualizacao, empresaatualizada.Portaria1510,
                empresaatualizada.Portaria671);

        }

        public async Task<EmpresaDto?> ObterPorIdAsync(int id, CancellationToken cancellationToken)
        {
            var empresa = await _repository.ObterPorIdAsync(id, cancellationToken) ?? new();

            return new EmpresaDto(empresa.Id, empresa.IdEndrereco, empresa.Codigo,
                           empresa.RazaoSocial,
                           empresa.CnpjCpf, empresa.Telefone,
                           empresa.Site, empresa.Email, empresa.TipoEmpresa,
                           empresa.DataCadastro, empresa.DataUltAtualizacao, empresa.Portaria1510,
                           empresa.Portaria671);
        }

        public async Task<IEnumerable<EmpresaDto>> ObterTodosAsync(CancellationToken cancellationToken)
        {
            var empresas = await _repository.ObterTodosAsync(cancellationToken);
            var empresasdto = empresas.Select(x => new EmpresaDto(x.Id, x.IdEndrereco, x.Codigo,
                x.RazaoSocial, x.CnpjCpf, x.Telefone, x.Site, x.Email, x.TipoEmpresa, x.DataCadastro, x.DataUltAtualizacao,
                x.Portaria1510, x.Portaria671));

            return empresasdto;
        }

        public async Task<bool> RemoverAsync(int id, CancellationToken cancellationToken)
        {
          var excluido =  await _repository.RemoverAsync(id, cancellationToken);
            return excluido;
        }
    }
}
