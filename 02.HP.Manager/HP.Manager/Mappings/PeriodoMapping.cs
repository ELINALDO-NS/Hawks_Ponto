using HP.Core.Entities;
using HP.Manager.DTOs.Periodo;
using Mapster;


namespace HP.Manager.Mappings
{
    public class PeriodoMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<PeriodoDto, Periodo>().TwoWays();
            config.NewConfig<ReabriPeridoDto, Periodo>().TwoWays();
            config.NewConfig<FechaPeriodoDto, Periodo>().TwoWays();
        }
    }
}
