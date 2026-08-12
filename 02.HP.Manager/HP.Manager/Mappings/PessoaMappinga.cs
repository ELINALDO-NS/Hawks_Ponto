using HP.Core.Entities;
using HP.Manager.DTOs.Pessoa;
using Mapster;


namespace HP.Manager.Mappings
{
    public class PessoaMapping : IRegister
    {
        void IRegister.Register(TypeAdapterConfig config)
        {
            config.NewConfig<PessoaDto, Pessoa>().TwoWays();
            config.NewConfig<AdicionaPessoaDto, Pessoa>().TwoWays();
            config.NewConfig<AtualizaPessoaDto, Pessoa>().TwoWays();

        }
    }
}
