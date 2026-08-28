using HP.Core.Entities;
using HP.Manager.DTOs.Marcacao;
using Mapster;


namespace HP.Manager.Mappings
{
    public class MarcacaoMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Marcacao, MarcacaoDto>().TwoWays();
            config.NewConfig<AdicionaMarcacaoDto, Marcacao>().TwoWays();
        }
    }
}
