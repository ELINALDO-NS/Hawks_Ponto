using HP.Core.Entities;
using HP.Manager.DTOs.Endereco;
using Mapster;


namespace HP.Manager.Mappings
{
    public class EnderecoMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<EnderecoDto,Endereco>().TwoWays();
           
        }
    }
}
