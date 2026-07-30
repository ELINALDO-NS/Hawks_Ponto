using HP.Core.Entities;
using HP.Manager.DTOs.Empresa;
using Mapster;


namespace HP.Manager.Mappings
{
    public class EmpresaMapping : IRegister
    {
        void IRegister.Register(TypeAdapterConfig config)
        {
            config.NewConfig<Empresa, EmpresaDto>().TwoWays();
            config.NewConfig<AdicionaEmpresaDto,Empresa>().TwoWays();
            config.NewConfig<AtualizaEmpresaDto, Empresa>().TwoWays();
            
        }
    }
}
