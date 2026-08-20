using HP.Core.Entities;
using HP.Core.Extentions;
using HP.Manager.DTOs.Cargo;
using HP.Manager.DTOs.Pessoa;
using Mapster;


namespace HP.Manager.Mappings
{
    public class PessoaMapping : IRegister
    {
        void IRegister.Register(TypeAdapterConfig config)
        {
            config.NewConfig<PessoaDto, Pessoa>();
            config.NewConfig<AdicionaPessoaDto, Pessoa>().TwoWays();
            config.NewConfig<Pessoa, PessoaDto>()
                .Map(dest => dest.Cargo, src => src.Cargos);
            config.NewConfig<AtualizaPessoaDto, Pessoa>();


        }
    }
}
