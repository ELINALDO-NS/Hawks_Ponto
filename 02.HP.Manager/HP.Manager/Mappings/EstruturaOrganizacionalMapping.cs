using HP.Core.Entities;
using HP.Manager.DTOs.EstruturaOrganizacional;
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

        }
    }
}
