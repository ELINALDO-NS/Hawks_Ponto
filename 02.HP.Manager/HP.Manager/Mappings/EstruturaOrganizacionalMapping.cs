using HP.Core.Entities;
using HP.Manager.DTOs.EstruturaOrganizacional;
using HP.Manager.DTOs.Pessoa;
using Mapster;

namespace HP.Manager.Mappings
{
    public class EstruturaOrganizacionalMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<EstruturaOrganizacionalDto, EstruturaOrganizacional>().TwoWays();
            config.NewConfig<AdicionaEstruturaOrganizacionalDto, EstruturaOrganizacional>().TwoWays();
            config.NewConfig<AtualizaEstruturaOrganizacionalDto, EstruturaOrganizacional>().TwoWays();

            config.NewConfig<EstruturaOrganizacionalPessoa, EstruturaOrganizacionalPessoaDto>()
             .Map(dest => dest.Id, src => src.EstruturaOrganizacionalId)
             .Map(dest => dest.Descricao, src => src.EstruturaOrganizacional.Descricao, src => src.EstruturaOrganizacional != null)
             .Map(dest => dest.Codigo, src => src.EstruturaOrganizacional.Codigo, src => src.EstruturaOrganizacional != null)
             .Map(dest => dest.DataInicio, src => src.DataInicio)
             .Map(dest => dest.DataFim, src => src.DataFim);

            config.NewConfig<EstruturaOrganizacionalPessoaDto, EstruturaOrganizacionalPessoa>()
                .Map(dest => dest.EstruturaOrganizacionalId, src => src.Id)
                .Map(dest => dest.DataInicio, src => src.DataInicio);

        }
    }
}
