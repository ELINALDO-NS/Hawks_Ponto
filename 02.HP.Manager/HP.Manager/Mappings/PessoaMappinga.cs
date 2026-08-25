using HP.Core.Entities;
using HP.Core.Extentions;
using HP.Manager.DTOs.Pessoa;
using Mapster;


namespace HP.Manager.Mappings
{
    public class PessoaMapping : IRegister
    {
        void IRegister.Register(TypeAdapterConfig config)
        {
            config.NewConfig<PessoaDto, Pessoa>();

            config.NewConfig<AdicionaPessoaDto, Pessoa>()
                .Map(dest => dest.Cpf, src => src.Cpf.RemoveFormatacao())
                .Map(dest => dest.Pis, src => src.Pis.RemoveFormatacao());

            config.NewConfig<Pessoa, PessoaDto>()
                .Map(dest => dest.Cargo, src => src.Cargos)
                .Map(dest => dest.EstruturaOrganizacional, src => src.EstruturasOrganizacionais)
                .Map(dest => dest.Horario, src => src.Horarios)
                .Map(dest => dest.Cpf, src => src.Cpf.FormatarCPF_CNPJ())
                .Map(dest => dest.Pis, src => src.Pis.FormataPis());
            
                config.NewConfig<AtualizaPessoaDto, Pessoa>()
                .Map(dest => dest.Cpf, src => src.Cpf.RemoveFormatacao())
                .Map(dest => dest.Pis, src => src.Pis.RemoveFormatacao());


        }
    }
}
