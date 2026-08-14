using HP.Core.Entities;
using HP.Manager.DTOs.Cargo;
using HP.Manager.DTOs.Pessoa;
using Mapster;


namespace HP.Manager.Mappings
{
    public class CargoMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CargoDto, Cargo>().TwoWays();
            config.NewConfig<AdicionaCargoDto, Cargo>().TwoWays();
            config.NewConfig<AtualizaCargoDto, Cargo>().TwoWays();
            config.NewConfig<CargoPessoaDto, Cargo>().TwoWays();
        }
    }
}
