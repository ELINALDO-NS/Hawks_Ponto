using HP.Core.Entities;
using HP.Manager.DTOs.Jornada;
using Mapster;


namespace HP.Manager.Mappings
{
    public class JornadaMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<JornadaDto, Jornada>().TwoWays();
            
        }
    }
}
