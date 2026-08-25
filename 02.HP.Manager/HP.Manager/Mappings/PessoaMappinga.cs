using HP.Core.Entities;
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
                .Map(dest => dest.Cargo, src => src.Cargos)
                .Map(dest => dest.EstruturaOrganizacional, src => src.EstruturasOrganizacionais);
            config.NewConfig<AtualizaPessoaDto, Pessoa>();


        }
    }
}
