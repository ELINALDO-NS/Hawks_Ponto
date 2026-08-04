using HP.Core.Entities;
using HP.Manager.DTOs.Empresa;
using HP.Manager.Interfaces.Managers;
using HP.Core.Interfaces.Repository;
using MapsterMapper;
using HP.Core.Extentions;


namespace HP.Manager.Implementation
{
    public class EmpresaManager(IEmpresaRepository _repository, IMapper _mapper) : IEmpresaManager
    {
        public async Task<EmpresaDto> AdicionarAsync(AdicionaEmpresaDto empresa, CancellationToken cancellationToken)
        {

            var novaempresa = _mapper.Map<Empresa>(empresa);
            novaempresa.CnpjCpf = novaempresa.CnpjCpf.RemoveFormatacao();
            await _repository.AdicionarAsync(novaempresa, cancellationToken);
            novaempresa.CnpjCpf = novaempresa.CnpjCpf.FormatarCPF_CNPJ();
            return _mapper.Map<EmpresaDto>(novaempresa);

        }

        public async Task<EmpresaDto> AtualizarAsync(AtualizaEmpresaDto empresa, CancellationToken cancellationToken)
        {

            var empresaatualizada = _mapper.Map<Empresa>(empresa);
            empresaatualizada.CnpjCpf = empresaatualizada.CnpjCpf.RemoveFormatacao();
            await _repository.AtualizarAsync(empresaatualizada, cancellationToken);
            empresaatualizada.CnpjCpf = empresaatualizada.CnpjCpf.FormatarCPF_CNPJ();
            return _mapper.Map<EmpresaDto>(empresaatualizada);

        }

        public async Task<EmpresaDto?> ObterPorIdAsync(int id, CancellationToken cancellationToken)
        {
            var empresa = await _repository.ObterPorIdAsync(id, cancellationToken);
            if (empresa is null)
            {
                return null;
            }
            empresa.CnpjCpf = empresa.CnpjCpf.FormatarCPF_CNPJ();
            return _mapper.Map<EmpresaDto>(empresa);
        }

        public async Task<IEnumerable<EmpresaDto>> ObterTodosAsync(CancellationToken cancellationToken)
        {
            var empresas = await _repository.ObterTodosAsync(cancellationToken);

            var empresasDto = empresas.Select(x => {
                x.CnpjCpf = x.CnpjCpf.FormatarCPF_CNPJ();
              return  _mapper.Map<EmpresaDto>(x); }).ToList();
            return empresasDto;
        }

        public async Task<bool> RemoverAsync(int id, CancellationToken cancellationToken)
        {
          var excluido =  await _repository.RemoverAsync(id, cancellationToken);
            return excluido;
        }
    }
}
