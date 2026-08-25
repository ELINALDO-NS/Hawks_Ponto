using HP.Core.Entities;
using HP.Manager.DTOs.Horario;
using HP.Manager.DTOs.Pessoa;
using Mapster;


namespace HP.Manager.Mappings
{
    public class HorarioMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<HorarioDto, Horario>().TwoWays();
            config.NewConfig<AdicionaHorarioDto, Horario>().TwoWays();
            config.NewConfig<AtualizaHorarioDto, Horario>().TwoWays();

            config.NewConfig<HorarioPessoa, HorarioPessoaDto>()
            .Map(dest => dest.Id, src => src.HorarioId)
            .Map(dest => dest.Descricao, src => src.Horario != null ? src.Horario.Descricao : null)
            .Map(dest => dest.Codigo, src => src.Horario != null ? src.Horario.Codigo : string.Empty)
            .Map(dest => dest.DataInicio, src => src.DataInicio)
            .Map(dest => dest.DataFim, src => src.DataFim);

            config.NewConfig<HorarioPessoaDto, HorarioPessoa>()
                .Map(dest => dest.HorarioId, src => src.Id)
                .Map(dest => dest.DataInicio, src => src.DataInicio);

        }
    }
}
