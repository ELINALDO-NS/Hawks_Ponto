using HP.Core.Entities;
using HP.Core.Extentions;
using HP.Manager.DTOs.Marcacao;
using Mapster;


namespace HP.Manager.Mappings
{
    public class MarcacaoMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Marcacao, MarcacaoDto>()
                .Map(dest => dest.CPF, src => src.CPF.FormatarCPF_CNPJ())
              .Map(dest => dest.PIS, src => src.PIS.FormatarCPF_CNPJ());

            config.NewConfig<AdicionaMarcacaoDto, Marcacao>()
              .Map(dest => dest.CPF, src => src.CPF.RemoveFormatacao())
              .Map(dest => dest.PIS, src => src.PIS.RemoveFormatacao());
        }
    }
}
