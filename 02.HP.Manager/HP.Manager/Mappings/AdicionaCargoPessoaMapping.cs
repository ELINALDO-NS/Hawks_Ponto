using HP.Core.Entities;
using HP.Manager.DTOs.Pessoa;
using Mapster;


namespace HP.Manager.Mappings
{
    public class AdicionaCargoPessoaMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<AdicionaCargoPessoaDto, CargoPessoa>()
                                 .TwoWays();
           
        }
    }
}
