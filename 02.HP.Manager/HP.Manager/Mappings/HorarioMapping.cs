using HP.Core.Entities;
using HP.Manager.DTOs.Horario;
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
            
        }
    }
}
