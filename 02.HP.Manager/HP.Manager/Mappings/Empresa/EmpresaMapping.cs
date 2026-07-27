using HP.Manager.DTOs.Empresa;
using Mapster;


namespace HP.Manager.Mappings.Empresa
{
    public class EmpresaMapping : IRegister
    {
        void IRegister.Register(TypeAdapterConfig config)
        {
            config.NewConfig<Core.Empresa, EmpresaDto>().TwoWays();
            config.NewConfig<AdicionaEmpresaDto,Core.Empresa>().TwoWays();
            config.NewConfig<AtualizaEmpresaDto, Core.Empresa>().TwoWays();
            
        }
    }
}
