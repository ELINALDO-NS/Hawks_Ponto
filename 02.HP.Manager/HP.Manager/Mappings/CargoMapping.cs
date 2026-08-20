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
            config.NewConfig<CargoPessoaDto, Cargo>();
            config.NewConfig<CargoPessoa, CargoPessoaDto>()
             .Map(dest => dest.Id, src => src.CargoId)
             .Map(dest => dest.Descricao, src => src.Cargo != null ? src.Cargo.Descricao : null)
             .Map(dest => dest.Codigo, src => src.Cargo != null ? src.Cargo.Codigo : 0)
             .Map(dest => dest.DataInicio, src => src.DataInicio)
             .Map(dest => dest.DataFim, src => src.DataFim);

            config.NewConfig<CargoPessoaDto, CargoPessoa>()
                .Map(dest => dest.CargoId, src => src.Id)
                .Map(dest => dest.DataInicio, src => src.DataInicio);
        }
    }
}
