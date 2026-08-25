using HP.Core.Entities;
using HP.Manager.DTOs.Pessoa;
using Mapster;


namespace HP.Manager.Mappings
{
    public class EstruturaOrganizacionaPessoaMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<AdicionaEstruturaOrganizacionalPessoaDto, EstruturaOrganizacional>()
                                .TwoWays();
        }
    }
}
