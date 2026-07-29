using HP.Manager.DTOs.Empresa;
using HP.Manager.DTOs.Endereco;
using System;
using System.Collections.Generic;
using System.Text;

namespace HP.Manager.Interfaces.Managers
{
    public interface IEnderecoManager
    {
        Task<EnderecoDto> AdicionarAsync(AdicionaEnderecoDto endereco, CancellationToken cancellationToken);
        Task<EnderecoDto> AtualizarAsync(AtualizaEnderecoDto endereco, CancellationToken cancellationToken);
        Task<bool> RemoverAsync(int id, CancellationToken cancellationToken);
        Task<EnderecoDto?> ObterPorIdAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<EnderecoDto>> ObterTodosAsync(CancellationToken cancellationToken);
    }
}
