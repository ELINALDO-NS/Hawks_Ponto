using HP.Manager.DTOs.Empresa;
using HP.Manager.DTOs.Endereco;
using Mapster;
using System;
using System.Collections.Generic;
using System.Text;

namespace HP.Manager.Mappings
{
    public class EnderecoMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<EnderecoDto,Core.Endereco>().TwoWays();
            config.NewConfig<AdicionaEnderecoDto, Core.Endereco>().TwoWays();
            config.NewConfig<AtualizaEnderecoDto, Core.Endereco>().TwoWays();
        }
    }
}
